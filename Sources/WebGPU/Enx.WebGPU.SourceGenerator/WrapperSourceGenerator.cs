using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Text;
using System.Collections.Immutable;
using System.Diagnostics;
using System.IO;
using System.Text;

namespace Enx.WebGPU.SourceGenerator;

[Generator]
public class WrapperSourceGenerator : IIncrementalGenerator
{
    static readonly string classCode = string.Empty;

    static WrapperSourceGenerator()
    {
        var assembly = typeof(WrapperSourceGenerator).Assembly;
        var stream = assembly.GetManifestResourceStream(assembly.GetName().Name + ".WrapperStruct.cs");
        using var streamReader = new StreamReader(stream);
        classCode = streamReader.ReadToEnd();
    }

    public void Initialize(IncrementalGeneratorInitializationContext context)
    {
        var assembly = typeof(WrapperSourceGenerator).Assembly;
        var stream = assembly.GetManifestResourceStream(assembly.GetName().Name + ".WrapperAttribute.cs");
        using (var streamReader = new StreamReader(stream))
        {
            var attributeTxt = streamReader.ReadToEnd();
            context.RegisterPostInitializationOutput((i) => i.AddSource("WrapperAttribute.g.cs", SourceText.From(attributeTxt, Encoding.UTF8)));
        }

        IncrementalValuesProvider<StructDeclarationSyntax> structDeclarations = context.SyntaxProvider
            .CreateSyntaxProvider(
                predicate: static (s, _) => IsSyntaxTargetForGeneration(s),
                transform: static (ctx, _) => GetSemanticTargetForGeneration(ctx))
            .Where(static m => m is not null)!;

        IncrementalValueProvider<(Compilation, ImmutableArray<StructDeclarationSyntax>)> compilationAndStruct
            = context.CompilationProvider.Combine(structDeclarations.Collect());

        context.RegisterSourceOutput(compilationAndStruct,
            static (spc, source) => Execute(source.Item1, source.Item2, spc));
    }

    static bool IsSyntaxTargetForGeneration(SyntaxNode node)
        => node is StructDeclarationSyntax m && m.AttributeLists.Count > 0;

    static StructDeclarationSyntax? GetSemanticTargetForGeneration(GeneratorSyntaxContext context)
    {
        // we know the node is a EnumDeclarationSyntax thanks to IsSyntaxTargetForGeneration
        var structDeclarationSyntax = (StructDeclarationSyntax)context.Node;

        // loop through all the attributes on the method
        foreach (AttributeListSyntax attributeListSyntax in structDeclarationSyntax.AttributeLists)
        {
            foreach (AttributeSyntax attributeSyntax in attributeListSyntax.Attributes)
            {
                if (context.SemanticModel.GetSymbolInfo(attributeSyntax).Symbol is not IMethodSymbol attributeSymbol)
                {
                    // weird, we couldn't get the symbol, ignore it
                    continue;
                }

                INamedTypeSymbol attributeContainingTypeSymbol = attributeSymbol.ContainingType;
                string fullName = attributeContainingTypeSymbol.ToDisplayString();

                // Is the attribute the [Wrapper] attribute?
                if (fullName == "Enx.WebGPU.SourceGenerator.WrapperAttribute")
                {
                    // return the enum
                    return structDeclarationSyntax;
                }
            }
        }

        // we didn't find the attribute we were looking for
        return null;
    }

    static void Execute(Compilation compilation, ImmutableArray<StructDeclarationSyntax> structs, SourceProductionContext context)
    {
        if (structs.IsDefaultOrEmpty) return;

        IEnumerable<StructDeclarationSyntax> distinctStruct = structs.Distinct();

        List<StructToGenerate> structToGenerates = GetTypesToGenerate(compilation, distinctStruct, context.CancellationToken);

        foreach (var structToGenerate in structToGenerates)
        {
            string str = GenerateExtensionClass(classCode, structToGenerate);
            context.AddSource(structToGenerate.StructName + ".g.cs", SourceText.From(str, Encoding.UTF8));
        }
    }

    static string GenerateExtensionClass(string classCode, StructToGenerate structToGenerate)
    {
        var structParts = structToGenerate.StructName.Split('.');
        var structName = structParts.Last();
        var namespaceS = string.Join(".", structParts, 0, structParts.Length - 1);

        var nativeParts = structToGenerate.TypeName.Split('.');
        var nativeName = nativeParts.Last();
        var nativeNamespace = string.Join(".", nativeParts, 0, nativeParts.Length - 1);

        var str = classCode;
        str = $"using {nativeNamespace};\n" + str;
        str = str.Replace("WrapperStruct", structName);
        str = str.Replace("Wrapped", structToGenerate.TypeName);
        str = str.Replace("XNamespace", namespaceS);
        str = str.Replace("Wraped", nativeName);

        return str;
    }

    static List<StructToGenerate> GetTypesToGenerate(Compilation compilation, IEnumerable<StructDeclarationSyntax> structs, CancellationToken ct)
    {
        var structsToGenerate = new List<StructToGenerate>();

        INamedTypeSymbol? attribute = compilation.GetTypeByMetadataName("Enx.WebGPU.SourceGenerator.WrapperAttribute");

        if (attribute is null) return structsToGenerate;

        foreach (StructDeclarationSyntax structDeclarationSyntax in structs)
        {
            ct.ThrowIfCancellationRequested();

            SemanticModel semanticModel = compilation.GetSemanticModel(structDeclarationSyntax.SyntaxTree);
            if (semanticModel.GetDeclaredSymbol(structDeclarationSyntax) is not INamedTypeSymbol structSymbol)
                continue;

            string structName = structSymbol.ToString();
            string nativeTypeName = string.Empty;

            foreach (AttributeData attributeData in structSymbol.GetAttributes())
            {
                if (!attribute.Equals(attributeData.AttributeClass, SymbolEqualityComparer.Default))
                    continue;

                if (attributeData.ConstructorArguments.Length != 1) break;

                var type = (INamedTypeSymbol)attributeData.ConstructorArguments[0].Value!;
                nativeTypeName = type.ToString();

                break;
            }

            structsToGenerate.Add(new StructToGenerate(structName, nativeTypeName));
        }

        return structsToGenerate;
    }

    public readonly struct StructToGenerate
    {
        public readonly string StructName;
        public readonly string TypeName;

        public StructToGenerate(string structName, string typeName)
        {
            StructName = structName;
            TypeName = typeName;
        }
    }
}
