using System.Collections.Generic;
using System.Linq;
using Secyud.Ugf;

namespace InfinityWorldChess.Ugf
{
    public static class IwcGlobalExtension
    {
        public static IList<TItem> GetVisible<TItem>(this IEnumerable<TItem> list)
        {
            return list.Where(u => u is IShowable).ToList();
        }
    }
}