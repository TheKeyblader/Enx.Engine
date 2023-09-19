using System.Linq;


public class ComponentsEnumeratorTemplate
{
    void Main(ICodegenContext context)
    {
        var parameterCounts = Enumerable.Range(2, 24).ToList();

        var model = new { ParemetersCounts = parameterCounts };

        context["Components.Iterators.generated.cs"]
            .WriteLine($$"""
            using Arch.Core;
            using System.Runtime.CompilerServices;
            namespace Enx.Engine.Arch {

                {{model.ParemetersCounts.Select(t => GenerateIterator(t))}}
            }
            """);

        context["Components.Enumerators.generated.cs"]
            .WriteLine($$"""
            using Arch.Core;
            using System.Runtime.CompilerServices;
            namespace Enx.Engine.Arch {

                {{model.ParemetersCounts.Select(t => GenerateEnumerator(t))}}
            }
            """);

        context["Components.QueryExtensions.generated.cs"]
            .WriteLine($$"""
            using Arch.Core;
            using System.Runtime.CompilerServices;
            namespace Enx.Engine.Arch {

                public static partial class QueryExtensions {
                    {{model.ParemetersCounts.Select(t => GenerateExtensions(t))}}
                }
            }
            """);
    }

    FormattableString GenerateIterator(int Count)
    {
        return $$"""
        [SkipLocalsInit]
        public readonly ref struct ComponentsIterator{{GenerateGenericParameters(Count)}} 
        {
            private readonly QueryChunkIterator _iterator;

            [SkipLocalsInit]
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public ComponentsIterator(QueryChunkIterator iterator)
            {
                _iterator = iterator;
            }

            [SkipLocalsInit]
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public ComponentsEnumerator{{GenerateGenericParameters(Count)}} GetEnumerator()
            {
                return new ComponentsEnumerator{{GenerateGenericParameters(Count)}}(_iterator.GetEnumerator());
            }
        }
        """;
    }

    FormattableString GenerateEnumerator(int Count)
    {
        return $$"""
        [SkipLocalsInit]
        public ref struct ComponentsEnumerator{{GenerateGenericParameters(Count)}}
        {
            private QueryChunkEnumerator _chunkEnumerator;
            private Components{{GenerateGenericParameters(Count)}} _first;
            private int _index;

            [SkipLocalsInit]
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public ComponentsEnumerator(QueryChunkEnumerator enumerator)
            {
                _chunkEnumerator = enumerator;

                if (_chunkEnumerator.MoveNext())
                {
                    _first = _chunkEnumerator.Current.GetFirst{{GenerateGenericParameters(Count)}}();
                    _index = _chunkEnumerator.Current.Size;
                }
            }

            [SkipLocalsInit]
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public bool MoveNext()
            {
                if (--_index >= 0)
                    return true;


                if (!_chunkEnumerator.MoveNext())
                    return false;

                _first = _chunkEnumerator.Current.GetFirst{{GenerateGenericParameters(Count)}}();
                _index = _chunkEnumerator.Current.Size - 1;
                return true;
            }

            [SkipLocalsInit]
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public void Reset()
            {
                _index = -1;
                _chunkEnumerator.Reset();

                if (_chunkEnumerator.MoveNext())
                    _index = _chunkEnumerator.Current.Size;
            }

            public readonly Components{{GenerateGenericParameters(Count)}} Current
            {
                [SkipLocalsInit]
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get
                {
                    {{GenerateUnsafeAdd(Count).Render()}}

                    return new Components{{GenerateGenericParameters(Count)}}({{GenerateRefParameters(Count)}});
                }
            }
        }
        """;
    }

    FormattableString GenerateExtensions(int Count)
    {
        return $$"""
         [MethodImpl(MethodImplOptions.AggressiveInlining)]
         public static ComponentsIterator{{GenerateGenericParameters(Count)}} GetComponentsIterator{{GenerateGenericParameters(Count)}}(this Query query)
         {
             return new ComponentsIterator{{GenerateGenericParameters(Count)}}(query.GetChunkIterator());
         }
         """;
    }

    IEnumerable<FormattableString> GenerateUnsafeAdd(int Count)
    {
        var parameterCounts = Enumerable.Range(0, Count).ToList();
        return parameterCounts.Select(x => (FormattableString)$$"""ref var t{{x}} = ref Unsafe.Add(ref _first.t{{x}}, _index);""");
    }

    FormattableString GenerateGenericParameters(int Count)
    {
        var parameterCounts = Enumerable.Range(0, Count).ToList();
        var joined = string.Join(",", parameterCounts.Select(p => "T" + p));
        return $$"""<{{joined}}>""";
    }

    FormattableString GenerateRefParameters(int Count)
    {
        var parameterCounts = Enumerable.Range(0, Count).ToList();
        var joined = string.Join(",", parameterCounts.Select(p => " ref t" + p));
        return $$"""{{joined}}""";
    }
}