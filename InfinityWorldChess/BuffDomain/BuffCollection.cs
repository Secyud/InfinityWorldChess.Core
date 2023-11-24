using System.Collections.Generic;
using InfinityWorldChess.FunctionDomain;
using Secyud.Ugf;

namespace InfinityWorldChess.BuffDomain
{
    public class BuffCollection<TTarget, TBuff> : BuffCollectionBase<TTarget, TBuff, int, int>
        where TBuff : class, IEquippable<TTarget>, IOverlayable<TTarget>, IHasId<int>
    {
        public BuffCollection(TTarget target) : base(target)
        {
        }

        protected override IDictionary<int, TBuff> InnerDictionary { get; }
            = new SortedDictionary<int, TBuff>();

        protected override int GetIndex(int key)
        {
            return key;
        }

        protected override int GetKey(int index)
        {
            return index;
        }
    }
}