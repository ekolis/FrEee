using System.Collections.Generic;

#nullable enable

public static class Extensions
{
    public static HashSet<T> ToHashSet<T>(
        this IEnumerable<T> source,
        IEqualityComparer<T>? comparer = null)
    {
        return new HashSet<T>(source, comparer);
    }
}
