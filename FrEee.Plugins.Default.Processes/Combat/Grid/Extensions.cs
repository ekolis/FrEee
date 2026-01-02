using System.Collections.Generic;

namespace FrEee.Plugins.Default.Processes.Combat.Grid;

public static class Extensions
{
    public static HashSet<T> ToHashSet<T>(
        this IEnumerable<T> source,
        IEqualityComparer<T> comparer = null)
    {
        return new HashSet<T>(source, comparer);
    }
}