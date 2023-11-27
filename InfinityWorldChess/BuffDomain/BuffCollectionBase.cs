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
            set
            {
                TBuff iBuff = value;
                TBuff oBuff = this[iBuff.Id];
                if (value is null)
                {
                    oBuff?.UnInstallFrom(Target);
                    InnerDictionary.Remove(GetKey(index));
                }
                else
                {
                    if (oBuff is null)
                    {
                        this[iBuff.Id] = iBuff;
                        iBuff.InstallFrom(Target);
                    }
                    else if (iBuff is not IHasPriority iPriority)
                    {
                    }
                    else if (oBuff is not IHasPriority oPriority ||
                             iPriority.Priority > oPriority.Priority)
                    {
                        oBuff.UnInstallFrom(Target);
                        iBuff.Overlay(oBuff);
                        iBuff.InstallFrom(Target);
                    }
                    else
                    {
                        oBuff.Overlay(iBuff);
                    }
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
                .Where(b=> b is not ILimitable limitable || limitable.CheckUseful())
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