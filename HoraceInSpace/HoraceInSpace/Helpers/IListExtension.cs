using System.Collections.Generic;
using System.Linq;

namespace HoraceInSpace;

/// <summary>
/// Extension class for IList
/// </summary>
public static class ListExtension
{
    /// <summary>
    /// Extension method allowing of deletion of several indexes at once.
    /// We delete things from the highest index down, to preserve their mapping to the items we want to delete.
    /// </summary>
    /// <param name="list">List we want to delete things from.</param>
    /// <param name="set">Set of Indexes that are to be deleted</param>
    /// <typeparam name="T"></typeparam>
    public static void MassDeleteFromHashset<T>(this IList<T> list, HashSet<int> set)
    {
        foreach (int i in set.OrderByDescending(x => x))
            list.RemoveAt(i);
    }
}