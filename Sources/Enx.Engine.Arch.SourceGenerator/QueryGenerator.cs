using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.Collections.Immutable;
using SourceGeneratorUtils;

namespace Enx.Engine.Arch.SourceGenerator;

[Generator]
public class QueryGenerator : IIncrementalGenerator
{
    public void Initialize(IncrementalGeneratorInitializationContext context)
    {
        IncrementalValuesProvider<MethodDeclarationSyntax> methodDeclarations = context.SyntaxProvider
            .CreateSyntaxProvider(
                predicate: static (s, _) => IsSyntaxTargetForGeneration(s),
                transform: static (ctx, _) => GetSemanticTargetForGeneration(ctx))
            .Where(static m => m is not null)!;

        IncrementalValueProvider<(Compilation, ImmutableArray<MethodDeclarationSyntax>)> compilationAndMethods
            = context.CompilationProvider.Combine(methodDeclarations.Collect());

        context.RegisterSourceOutput(compilationAndMethods,
            static (spc, source) => Execute(source.Item1, source.Item2, spc));
    }

    static bool IsSyntaxTargetForGeneration(SyntaxNode node)
    => node is MethodDeclarationSyntax m && m.AttributeLists.Count > 0;

    static MethodDeclarationSyntax? GetSemanticTargetForGeneration(GeneratorSyntaxContext context)
    {
        // we know the node is a EnumDeclarationSyntax thanks to IsSyntaxTargetForGeneration
        var methodDeclarationSyntax = (MethodDeclarationSyntax)context.Node;

        // loop through all the attributes on the method
        foreach (AttributeListSyntax attributeListSyntax in methodDeclarationSyntax.AttributeLists)
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

                // Is the attribute the [Query] attribute?
                if (fullName == "Enx.Engine.Arch.QueryAttribute")
                {
                    // return the enum
                    return methodDeclarationSyntax;
                }
            }
        }

        // we didn't find the attribute we were looking for
        return null;
    }

    static void Execute(Compilation compilation, ImmutableArray<MethodDeclarationSyntax> methods, SourceProductionContext context)
    {
        if (methods.IsDefaultOrEmpty) return;

        IEnumerable<MethodDeclarationSyntax> distinctMethods = methods.Distinct();

        List<QuerySpec> specs = GetTypesToGenerate(compilation, context, distinctMethods, context.CancellationToken);

        foreach (var spec in specs)
        {
            var source = new QueryFileEmitter();
            var sourceFile = source.GenerateSource(spec);
            context.AddSource(sourceFile.Name, sourceFile.Content.ToSourceText());
        }
    }

    static List<QuerySpec> GetTypesToGenerate(Compilation compilation, SourceProductionContext context, IEnumerable<MethodDeclarationSyntax> methods, CancellationToken ct)
    {
        var specs = new List<QuerySpec>();

        var queryAttribute = compilation.GetBestTypeByMetadataName("Enx.Engine.Arch.QueryAttribute");
        var systemAttribute = compilation.GetBestTypeByMetadataName("Enx.Engine.Arch.SystemAttribute");
        var entityType = compilation.GetBestTypeByMetadataName("Arch.Core.Entity");
        var attributeToCopy = new[]
        {
            compilation.GetBestTypeByMetadataName("Enx.Engine.Arch.UpdateAfterAttribute`1"),
            compilation.GetBestTypeByMetadataName("Enx.Engine.Arch.UpdateBeforeAttribute`1"),
            compilation.GetBestTypeByMetadataName("Enx.Engine.Arch.UpdateInGroupAttribute`1"),
            compilation.GetBestTypeByMetadataName("Enx.Engine.Arch.SystemAttribute"),
        };

        var filterAttribute = new[]
        {
            compilation.GetBestTypeByMetadataName("Enx.Engine.Arch.AllAttribute"),
            compilation.GetBestTypeByMetadataName("Enx.Engine.Arch.AnyAttribute"),
            compilation.GetBestTypeByMetadataName("Enx.Engine.Arch.NoneAttribute"),
            compilation.GetBestTypeByMetadataName("Enx.Engine.Arch.ExclusiveAttribute"),
        };

        if (queryAttribute is null) return specs;
        if (systemAttribute is null) return specs;
        if (entityType is null) return specs;

        foreach (var methodDeclaration in methods)
        {
            ct.ThrowIfCancellationRequested();

            SemanticModel semanticModel = compilation.GetSemanticModel(methodDeclaration.SyntaxTree);
            if (semanticModel.GetDeclaredSymbol(methodDeclaration) is not IMethodSymbol methodSymbol)
                continue;

            var found = false;
            var hasSystemAttribute = false;

            var attributeCopied = new List<string>();

            var allParams = new List<ITypeSymbol>();
            var anyParams = new List<ITypeSymbol>();
            var noneParams = new List<ITypeSymbol>();
            var exclusiveParams = new List<ITypeSymbol>();

            foreach (var attribute in methodSymbol.GetAttributes())
            {
                if (queryAttribute.Equals(attribute.AttributeClass, SymbolEqualityComparer.Default))
                {
                    found = true;
                    continue;
                }

                if (systemAttribute.Equals(attribute.AttributeClass, SymbolEqualityComparer.Default))
                {
                    hasSystemAttribute = true;
                    continue;
                }

                if (attributeToCopy.Contains(attribute.AttributeClass?.OriginalDefinition, SymbolEqualityComparer.Default))
                {
                    attributeCopied.Add(attribute.ToString());
                    continue;
                }

                if (filterAttribute[0]!.Equals(attribute.AttributeClass, SymbolEqualityComparer.Default))
                {
                    GetAttributeTypes(attribute, allParams);
                }

                if (filterAttribute[1]!.Equals(attribute.AttributeClass, SymbolEqualityComparer.Default))
                {
                    GetAttributeTypes(attribute, anyParams);
                }

                if (filterAttribute[2]!.Equals(attribute.AttributeClass, SymbolEqualityComparer.Default))
                {
                    GetAttributeTypes(attribute, noneParams);
                }

                if (filterAttribute[3]!.Equals(attribute.AttributeClass, SymbolEqualityComparer.Default))
                {
                    GetAttributeTypes(attribute, exclusiveParams);
                }
            }

            if (!found) continue;
            if (hasSystemAttribute && !methodSymbol.IsStatic)
            {
                context.ReportDiagnostic(Diagnostic.Create(
                    new DiagnosticDescriptor(
                        "EEASG001",
                        "Method with system attribte should be static",
                        "Method {0} should be static",
                        "SystemBase",
                        DiagnosticSeverity.Error,
                        true), methodSymbol.Locations.FirstOrDefault(), methodSymbol.Name));
                continue;
            }

            var services = new List<IParameterSymbol>();
            var components = new List<IParameterSymbol>();
            var isEntityQuery = false;
            foreach (var @params in methodSymbol.Parameters)
            {
                if (@params.Type.Equals(entityType, SymbolEqualityComparer.Default))
                    isEntityQuery = true;
                else if (@params.RefKind == RefKind.None)
                    services.Add(@params);
                else
                {
                    components.Add(@params);
                    allParams.Add(@params.Type);
                }
            }

            var typeDesc = TypeDescHelpers.CreateForPartialClass(methodSymbol.ContainingType);
            var typedeclarations = typeDesc.GetTypeDeclarationWithContainingTypes().ToList();

            if (hasSystemAttribute)
            {
                typedeclarations.Insert(0, TypeDesc.Create(
                    methodSymbol.Name + "System",
                    isPartial: true,
                    accessibility: Accessibility.Public,
                    typeKind: TypeKind.Class,
                    baseTypes: [TypeDesc.Create("SystemBase")]).ToTypeDeclaration());
            }


            specs.Add(new QuerySpec
            {
                Namespace = methodSymbol.ContainingNamespace.IsGlobalNamespace ? null : methodSymbol.ContainingNamespace.ToDisplayString(),
                QueryMethod = methodSymbol,
                TypeDeclarations = new ImmutableEquatableArray<string>(typedeclarations),
                AttributesToCopy = hasSystemAttribute ? attributeCopied : new List<string>(),
                Components = components,
                IsEntityQuery = isEntityQuery,
                Services = services,
                IsSystemQuery = hasSystemAttribute,
                Parameters = methodSymbol.Parameters,
                AllFilteredTypes = allParams,
                AnyFilteredTypes = anyParams,
                ExclusiveFilteredTypes = exclusiveParams,
                NoneFilteredTypes = noneParams
            });
        }

        return specs;
    }

    public static void GetAttributeTypes(AttributeData data, List<ITypeSymbol> array)
    {
        if (data is null) return;

        var constructorArguments = data.ConstructorArguments[0].Values;
        var constructrorArgumentsTypes = constructorArguments.Select(c => c.Value as ITypeSymbol).ToArray();
        array.AddRange(constructrorArgumentsTypes!);
    }
}
