using System;
using System.Collections.Generic;
using InfinityWorldChess.FunctionDomain;
using Secyud.Ugf;

namespace InfinityWorldChess.BuffDomain
{
    public class PropertyCollection<TTarget,TBuff>:
        BuffCollectionBase<TTarget,TBuff,Type,Guid> 
        where TBuff : class, IInstallable<TTarget>, IOverlayable<TTarget>, IHasId<Type>
    {
        public PropertyCollection(TTarget target) : base(target)
        {
        }

        protected override IDictionary<Guid, TBuff> InnerDictionary { get; }
            = new SortedDictionary<Guid, TBuff>();
        
        protected override Type GetIndex(Guid key)
        {
            return U.Tm[key].Type;
        }

        protected override Guid GetKey(Type index)
        {
            return index.GUID;
        }
        
        public TProperty GetOrCreate<TProperty>()
            where TProperty : class, TBuff
        {
            TProperty ret = Get<TProperty>();

            if (ret is null)
            {
                ret = U.Get<TProperty>();
                this[typeof(TProperty)] = ret;
            }
            
            return ret;
        }

        public TPropertyBuff Get<TPropertyBuff>()
            where TPropertyBuff : class, TBuff
        {
            return this[typeof(TPropertyBuff)] as TPropertyBuff;
        }
    }
}