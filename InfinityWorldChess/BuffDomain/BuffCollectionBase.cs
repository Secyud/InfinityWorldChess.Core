#region

using System.Collections.Generic;
using System.Linq;
using InfinityWorldChess.FunctionDomain;
using InfinityWorldChess.GlobalDomain;
using Secyud.Ugf;
using Secyud.Ugf.Archiving;

#endregion

namespace InfinityWorldChess.BuffDomain
{
    public abstract class BuffCollectionBase<TTarget, TBuff, TIndex, TKey>
        where TBuff : class, IInstallable<TTarget>, IOverlayable<TTarget>, IHasId<TIndex>
    {
        protected abstract IDictionary<TKey, TBuff> InnerDictionary { get; }
        protected virtual TTarget Target { get; }

        protected BuffCollectionBase(TTarget target)
        {
            Target = target;
        }

        public TBuff this[TIndex index]
        {
            get
            {
                InnerDictionary.TryGetValue(GetKey(index), out TBuff buff);
                return buff;
            }
            protected set
            {
                TBuff buff = this[index];

                if (value == buff) return;

                if (value is null)
                {
                    buff.UnInstallFrom(Target);
                    InnerDictionary.Remove(GetKey(index));
                }
                else if (buff is null)
                {
                    InnerDictionary[GetKey(index)] = value;
                    value.InstallFrom(Target);
                }
                else if (value is not IHasPriority iPriority)
                {
                }
                else if (buff is not IHasPriority oPriority ||
                         iPriority.Priority > oPriority.Priority)
                {
                    InnerDictionary[GetKey(index)] = value;
                    buff.UnInstallFrom(Target);
                    value.Overlay(buff);
                    value.InstallFrom(Target);
                }
                else
                {
                    buff.Overlay(value);
                }
            }
        }

        protected abstract TIndex GetIndex(TKey key);
        protected abstract TKey GetKey(TIndex index);

        public virtual void Install(TBuff buff)
        {
            this[buff.Id] = buff;
        }

        public virtual void UnInstall(TIndex id)
        {
            this[id] = null;
        }

        public virtual void Save(IArchiveWriter writer)
        {
            List<TBuff> buffs = InnerDictionary
                .Values
                .Where(b => b is IArchivable)
                .Where(b => b is not ILimitable limitable || limitable.CheckUseful())
                .ToList();

            writer.Write(buffs.Count);
            foreach (TBuff buff in buffs)
            {
                writer.WriteObject(buff);
            }
        }

        public virtual void Load(IArchiveReader reader)
        {
            Clear();
            int count = reader.ReadInt32();
            for (int i = 0; i < count; i++)
            {
                TBuff buff = reader.ReadObject<TBuff>();
                Install(buff);
            }
        }

        public IEnumerable<TBuff> All()
        {
            return InnerDictionary.Values;
        }

        public IList<TBuff> AllVisible()
        {
            return All().GetVisible();
        }

        public void Clear()
        {
            foreach (TBuff buff in InnerDictionary.Values)
            {
                buff.UnInstallFrom(Target);
            }

            InnerDictionary.Clear();
        }
    }
}