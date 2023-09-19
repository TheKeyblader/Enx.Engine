using Microsoft.CodeAnalysis;
using SourceGeneratorUtils;
using System.ComponentModel;
using System.Text;

namespace Enx.Engine.Arch.SourceGenerator;

public record QueryEmitter : SourceCodeEmitter<QuerySpec>
{
    public override void EmitTargetSourceCode(QuerySpec target, SourceWriter writer)
    {
        writer.WriteEmptyLines(1);
        if (target.IsSystemQuery)
        {
            GenerateSystemServicesFields(target, writer);
            GenerateSystemConstructor(target, writer);
            GenerateSystemUpdate(target, writer);
        }

        if (target.HasWorld) GenerateQueryFields(target, writer);
        GenerateQuery(target, writer);
    }

    static void GenerateSystemServicesFields(QuerySpec target, SourceWriter writer)
    {
        foreach (var service in target.Services)
        {
            writer.WriteLine($"private readonly {service.Type} _service{service.Ordinal};");
        }

        writer.WriteEmptyLines(1);
    }
    static void GenerateSystemConstructor(QuerySpec target, SourceWriter writer)
    {
        var paramters = new StringBuilder();
        foreach (var service in target.Services)
        {
            paramters.Append($", {service.Type} service{service.Ordinal}");
        }

        writer.WriteLine($"public {target.QueryMethod.Name}System(World world{paramters}) : base(world)");
        writer.OpenBlock();
        foreach (var service in target.Services)
            writer.WriteLine($"_service{service.Ordinal} = service{service.Ordinal};");
        writer.CloseBlock();
        writer.WriteEmptyLines(1);
    }
    static void GenerateSystemUpdate(QuerySpec target, SourceWriter writer)
    {
        writer.WriteLine("public override void Update()");
        writer.OpenBlock();

        writer.WriteLine($"{target.QueryMethod.Name}Query();");

        writer.CloseBlock();
        writer.WriteEmptyLines(1);
    }

    static void GenerateQueryFields(QuerySpec target, SourceWriter writer)
    {
        var @static = target.QueryMethod.IsStatic && !target.IsSystemQuery ? "static " : "";

        writer.WriteLine($"private {@static}QueryDescription {target.QueryMethod.Name}_QueryDescription = new QueryDescription");
        writer.OpenBlock();
        writer.WriteLine($"All = {GetTypeArray(target.AllFilteredTypes)},");
        writer.WriteLine($"Any = {GetTypeArray(target.AnyFilteredTypes)},");
        writer.WriteLine($"None = {GetTypeArray(target.NoneFilteredTypes)},");
        writer.WriteLine($"Exclusive = {GetTypeArray(target.ExclusiveFilteredTypes)},");
        writer.Indentation--; writer.WriteLine("};");
    }

    static void GenerateQuery(QuerySpec target, SourceWriter writer)
    {
        var @static = target.QueryMethod.IsStatic && !target.IsSystemQuery ? "static " : "";

        var world = target.HasWorld && !target.IsSystemQuery ? "World world" : string.Empty;
        var separator = target.HasWorld && target.HasServices && !target.IsSystemQuery ? ", " : string.Empty;
        var servicePrefix = target.IsSystemQuery ? "_" : string.Empty;
        var worldVar = target.IsSystemQuery ? "World" : "world";
        var serviceParams = string.Empty;

        if (!target.IsSystemQuery)
        {
            for (var i = 0; i < target.Services.Count; i++)
            {
                var service = target.Services[i];
                if (i != 0) serviceParams += ", ";
                serviceParams += $"{service.Type} service{service.Ordinal}";
            }
        }

        writer.WriteLine("[MethodImpl(MethodImplOptions.AggressiveInlining)]");
        writer.WriteLine($"public {@static}void {target.QueryMethod.Name}Query({world}{separator}{serviceParams})");
        writer.OpenBlock();

        if (target.HasWorld)
        {
            writer.WriteLine($"var query = {worldVar}.Query({target.QueryMethod.Name}_QueryDescription);");
            writer.WriteLine("foreach(ref var chunk in query.GetChunkIterator())");
            writer.OpenBlock();
            if (target.IsEntityQuery)
                writer.WriteLine("ref var entityFirstElement = ref chunk.Entity(0);");
            foreach (var component in target.Components)
                writer.WriteLine($"ref var component{component.Ordinal}FirstElement = ref chunk.GetFirst<{component.Type}>();");

            writer.WriteLine("foreach(var entityIndex in chunk)");
            writer.OpenBlock();

            if (target.IsEntityQuery)
                writer.WriteLine("ref readonly var entity = ref Unsafe.Add(ref entityFirstElement, entityIndex);");

            foreach (var component in target.Components)
                writer.WriteLine($"ref var component{component.Ordinal} = ref Unsafe.Add(ref component{component.Ordinal}FirstElement, entityIndex);");
        }

        var callParams = string.Empty;

        for (var i = 0; i < target.Parameters.Count; i++)
        {
            var param = target.Parameters[i];
            if (i != 0) callParams += ", ";

            callParams += RefKindToString(param.RefKind) + " ";

            if (param.Type.Name == "Entity")
                callParams += "entity";
            else if (param.RefKind == RefKind.None)
                callParams += $"{servicePrefix}service{param.Ordinal}";
            else callParams += $"component{param.Ordinal}";
        }

        writer.WriteLine($"{target.QueryMethod.Name}({callParams});");

        if (target.HasWorld)
        {
            writer.CloseBlock();
            writer.CloseBlock();
        }


        writer.CloseBlock();
    }

    static string RefKindToString(RefKind refKind) => refKind switch
    {
        RefKind.None => "",
        RefKind.Ref => "ref",
        RefKind.In => "in",
        RefKind.Out => "out",
        _ => "",
    };


    static string GetTypeArray(IList<ITypeSymbol> parameterSymbols)
    {
        var sb = new StringBuilder();

        if (parameterSymbols.Count == 0)
        {
            sb.Append("Array.Empty<ComponentType>()");
            return sb.ToString();
        }

        sb.Append("new ComponentType[]{");

        foreach (var symbol in parameterSymbols)
            if (symbol.Name is not "Entity") // Prevent entity being added to the type array
                sb.Append($"typeof({symbol}),");

        if (sb.Length > 0) sb.Length -= 1;
        sb.Append('}');

        return sb.ToString();
    }

    public override IEnumerable<string> GetAttributesToApply(QuerySpec target)
    {
        return target.AttributesToCopy;
    }

    public override IEnumerable<string> GetOuterUsingDirectives(QuerySpec target)
    {
        yield return "System";
        yield return "System.Runtime.CompilerServices";
        yield return "System.Runtime.InteropServices";
        yield return "Arch.Core";
        yield return "Arch.Core.Extensions";
        yield return "Arch.Core.Utils";
        yield return "ArrayExtensions = CommunityToolkit.HighPerformance.ArrayExtensions";
        yield return "Component = Arch.Core.Utils.Component";
        yield return "Enx.Engine.Arch";
    }
}
