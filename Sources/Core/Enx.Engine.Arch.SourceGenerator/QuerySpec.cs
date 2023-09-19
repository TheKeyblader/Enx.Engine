using Microsoft.CodeAnalysis;
using SourceGeneratorUtils;

namespace Enx.Engine.Arch.SourceGenerator;

public record QuerySpec : AbstractTypeGenerationSpec
{
    public required IMethodSymbol QueryMethod { get; set; }
    public IList<IParameterSymbol> Services { get; set; } = new List<IParameterSymbol>();
    public IList<IParameterSymbol> Components { get; set; } = new List<IParameterSymbol>();
    public IList<IParameterSymbol> Parameters { get; set; } = new List<IParameterSymbol>();
    public IList<string> AttributesToCopy { get; set; } = new List<string>();

    public IList<ITypeSymbol> AllFilteredTypes { get; set; } = new List<ITypeSymbol>();
    public IList<ITypeSymbol> AnyFilteredTypes { get; set; } = new List<ITypeSymbol>();
    public IList<ITypeSymbol> NoneFilteredTypes { get; set; } = new List<ITypeSymbol>();
    public IList<ITypeSymbol> ExclusiveFilteredTypes { get; set; } = new List<ITypeSymbol>();

    public bool IsSystemQuery { get; set; }
    public bool IsEntityQuery { get; set; }
    public bool HasServices => Services.Count > 0;
    public bool HasComponents => Components.Count > 0;
    public bool HasWorld => HasComponents || IsEntityQuery;
}
