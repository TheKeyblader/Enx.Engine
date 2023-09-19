using SourceGeneratorUtils;

namespace Enx.Engine.Arch.SourceGenerator;
public class QueryFileEmitter : SourceFileEmitter<QuerySpec>
{
    public QueryFileEmitter() : base(new List<SourceCodeEmitter<QuerySpec>>() { new QueryEmitter() })
    {

    }

    public override string GetFileName(QuerySpec target) => $"{target.QueryMethod.ContainingType.Name}-{target.QueryMethod.Name}.g.cs";
}
