#region

using System;
using System.Collections.Generic;
using System.Linq;
using Secyud.Ugf;
using Secyud.Ugf.Archiving;

#endregion

namespace InfinityWorldChess.BuffDomain
{
    public class TypeBuffProperty<TTarget> : Dictionary<Type, IBuff<TTarget>>
    {
        public TypeBuffProperty(TTarget target)
        {
            Target = target;
        }
        
        protected TTarget Target { get; }

        public virtual TBuff Get<TBuff>()
            where TBuff : class, IBuff<TTarget>
        {
            return Get(typeof(TBuff)) as TBuff;
        }

        public virtual IBuff<TTarget> Get(Type type)
        {
            return TryGetValue(type, out IBuff<TTarget> buff) ? buff : null;
        }

        public virtual void Install(IBuff<TTarget> buff)
        {
            Type type = buff.GetType();
            if (TryGetValue(type, out IBuff<TTarget> oBuff))
            {
                oBuff.UnInstall(Target);
                buff.Overlay(oBuff);
                buff.Install(Target);
            }
            else
            {
                this[type] = buff;
                buff.Install(Target);
            }
        }

        public virtual TBuff GetOrInstall<TBuff>()
            where TBuff : class, IBuff<TTarget>
        {
            return GetOrInstall(typeof(TBuff)) as TBuff;
        }

        public virtual IBuff<TTarget> GetOrInstall(Type type)
        {
            if (TryGetValue(type, out IBuff<TTarget> buff))
            {
                return buff;
            }

            buff = U.Get(type) as IBuff<TTarget>;
            this[type] = buff;
            buff!.Install(Target);
            return buff;
        }

        public virtual void UnInstall<TBuff>()
            where TBuff : class, IBuff<TTarget>
        {
            UnInstall(typeof(TBuff));
        }

        public virtual void UnInstall(Type type)
        {
            if (TryGetValue(type, out IBuff<TTarget> buff))
            {
                buff.UnInstall(Target);
            }
        }

        public bool Contains<TBuff>() where TBuff : class, IBuff<TTarget>
        {
            return Contains(typeof(TBuff));
        }
        
        public bool Contains(Type type)
        {
            return ContainsKey(type);
        }

        public List<IBuffShowable<TTarget>> GetVisibleBuff()
        {
            return Values
                .Where(u => u is IBuffShowable<TTarget> { Visible: true })
                .Cast<IBuffShowable<TTarget>>()
                .ToList();
        }


        public virtual void Save(IArchiveWriter writer)
        {
            IBuff<TTarget>[] buffs = Values
                .Where(b => b is IArchivable)
                .ToArray();

            writer.Write(buffs.Length);
            foreach (IBuff<TTarget> buff in buffs)
                writer.WriteObject(buff);
        }

        public virtual void Load(IArchiveReader reader)
        {
            Clear();
            int count = reader.ReadInt32();
            for (int i = 0; i < count; i++)
            {
                IBuff<TTarget> buff = reader.ReadObject<IBuff<TTarget>>();
                Install(buff);
            }
        }

        public new void Clear()
        {
            foreach (IBuff<TTarget> buff in Values)
                buff.UnInstall(Target);
            base.Clear();
        }
    }
}