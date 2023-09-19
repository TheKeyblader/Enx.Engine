using System.Linq;


public class EntityComponentsEnumeratorTemplate
{
    void Main(ICodegenContext context)
    {
        var parameterCounts = Enumerable.Range(2, 24).ToList();

        var model = new { ParemetersCounts = parameterCounts };

        context["EntityComponents.Iterators.generated.cs"]
            .WriteLine($$"""
            using Arch.Core;
            using System.Runtime.CompilerServices;
            namespace Enx.Engine.Arch {

                {{model.ParemetersCounts.Select(t => GenerateIterator(t))}}
            }
            """);

        context["EntityComponents.Enumerators.generated.cs"]
            .WriteLine($$"""
            using Arch.Core;
            using System.Runtime.CompilerServices;
            namespace Enx.Engine.Arch {

                {{model.ParemetersCounts.Select(t => GenerateEnumerator(t))}}
            }
            """);

        context["EntityComponents.QueryExtensions.generated.cs"]
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
        public readonly ref struct EntityComponentsIterator{{GenerateGenericParameters(Count)}} 
        {
            private readonly QueryChunkIterator _iterator;

            [SkipLocalsInit]
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public EntityComponentsIterator(QueryChunkIterator iterator)
            {
                _iterator = iterator;
            }

            [SkipLocalsInit]
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public EntityComponentsEnumerator{{GenerateGenericParameters(Count)}} GetEnumerator()
            {
                return new EntityComponentsEnumerator{{GenerateGenericParameters(Count)}}(_iterator.GetEnumerator());
            }
        }
        """;
    }

    FormattableString GenerateEnumerator(int Count)
    {
        return $$"""
        [SkipLocalsInit]
        public ref struct EntityComponentsEnumerator{{GenerateGenericParameters(Count)}}
        {
            private QueryChunkEnumerator _chunkEnumerator;
            private ref Entity _firstEntity;
            private Components{{GenerateGenericParameters(Count)}} _first;
            private int _index;

            [SkipLocalsInit]
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public EntityComponentsEnumerator(QueryChunkEnumerator enumerator)
            {
                _chunkEnumerator = enumerator;

                if (_chunkEnumerator.MoveNext())
                {
                    _firstEntity = _chunkEnumerator.Current.Entity(0);
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

                _firstEntity = _chunkEnumerator.Current.Entity(0);
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

            public readonly EntityComponents{{GenerateGenericParameters(Count)}} Current
            {
                [SkipLocalsInit]
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get
                {
                    ref var entity = ref Unsafe.Add(ref _firstEntity, _index);
                    {{GenerateUnsafeAdd(Count).Render()}}

                    return new EntityComponents{{GenerateGenericParameters(Count)}}(ref entity,{{GenerateRefParameters(Count)}});
                }
            }
        }
        """;
    }

    FormattableString GenerateExtensions(int Count)
    {
        return $$"""
         [MethodImpl(MethodImplOptions.AggressiveInlining)]
         public static EntityComponentsIterator{{GenerateGenericParameters(Count)}} GetEntityComponentsIterator{{GenerateGenericParameters(Count)}}(this Query query)
         {
             return new EntityComponentsIterator{{GenerateGenericParameters(Count)}}(query.GetChunkIterator());
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