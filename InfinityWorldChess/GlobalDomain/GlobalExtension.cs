using System.Collections.Generic;
using System.Linq;
using Secyud.Ugf;

namespace InfinityWorldChess.GlobalDomain
{
    public static class GlobalExtension
    {
        public static IList<TItem> GetVisible<TItem>(this IEnumerable<TItem> list)
        {
            return list.Where(u => u is IShowable).ToList();
        }
    }
}