using Microsoft.CodeAnalysis;
using SourceGeneratorUtils;

namespace Enx.Engine.Arch.SourceGenerator;

public static class TypeDescHelpers
{
    public static TypeDesc CreateForPartialClass(INamedTypeSymbol type)
    {
        return TypeDesc.Create(
            type.Name,
            type.ContainingNamespace.Name,
            type.TypeKind,
            type.SpecialType,
            type.DeclaredAccessibility,
            type.IsValueType,
            type.IsReadOnly,
            type.IsAbstract,
            true,
            type.IsRecord,
            type.IsStatic,
            type.IsSealed,
            genericTypes: CreateTypeParamForClass(type).ToArray(),
            containingTypes: GetContainingType(type).ToArray());
    }

    public static IEnumerable<TypeDesc> CreateTypeParamForClass(INamedTypeSymbol type)
    {
        foreach (var param in type.TypeParameters)
        {
            yield return TypeDesc.Create(param.Name);
        }
    }

    public static IEnumerable<TypeDesc> GetContainingType(INamedTypeSymbol type)
    {
        INamedTypeSymbol containing = type.ContainingType;
        while (containing != null)
        {
            yield return CreateForPartialClass(containing);
            containing = containing.ContainingType;
        }
    }
}
