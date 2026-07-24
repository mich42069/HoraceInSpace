using System.Collections.Generic;
using System.Linq;

namespace HoraceInSpace;

public static class ListExtension
{
    public static void MassDeleteFromHashset<T>(this IList<T> list, HashSet<int> set)
    {
        foreach (int i in set.OrderByDescending(x => x))
            list.RemoveAt(i);
    }
}