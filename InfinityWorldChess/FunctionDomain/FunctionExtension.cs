using System.Collections.Generic;
using InfinityWorldChess.GlobalDomain;

namespace InfinityWorldChess.FunctionDomain
{
    public static class FunctionExtension
    {
        public static void InvokeList<TActionable, TTarget>(this List<TActionable> actions, TTarget target)
            where TActionable : class, IActionable<TTarget>
        {
            actions.Sort(Compare);

            foreach (TActionable action in actions)
            {
                action.Invoke(target);
            }
        }
        
        public static void InvokeList<TActionable>(this List<TActionable> actions)
            where TActionable : class, IActionable
        {
            actions.Sort(Compare);

            foreach (TActionable action in actions)
            {
                action.Invoke();
            }
        }

        private static int Compare(object lft, object rht)
        {
            int lftPriority = 0, rhtPriority = 0;

            if (lft is IHasPriority lPriority)
            {
                lftPriority = lPriority.Priority;
            }

            if (rht is IHasPriority rPriority)
            {
                rhtPriority = rPriority.Priority;
            }

            return lftPriority - rhtPriority;
        }
    }
}