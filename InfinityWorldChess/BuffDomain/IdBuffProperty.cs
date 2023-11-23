#region

using System.Collections.Generic;
using System.Linq;
using Secyud.Ugf.Archiving;

#endregion

namespace InfinityWorldChess.BuffDomain
{
    public class IdBuffProperty<TTarget> : SortedDictionary<int, IBuff<TTarget>>
    {
        protected virtual TTarget Target { get; }

        public IdBuffProperty(TTarget target)
        {
            Target = target;
        }

        public IdBuffProperty()
        {
            
        }
        
        public virtual TBuff Get<TBuff>(int id)
            where TBuff : class, IBuff<TTarget>
        {
            return Get(id) as TBuff;
        }

        public virtual IBuff<TTarget> Get(int id)
        {
            return TryGetValue(id, out IBuff<TTarget> buff) ? buff : null;
        }

        public virtual void Install(IBuff<TTarget> buff)
        {
            if (TryGetValue(buff.Id, out IBuff<TTarget> oBuff))
            {
                if (buff.BuffLevel > oBuff.BuffLevel)
                {
                    oBuff.UnInstall(Target);
                    buff.Overlay(oBuff);
                    buff.Install(Target);
                }
                else
                {
                    oBuff.Overlay(buff);
                }
            }
            else
            {
                this[buff.Id] = buff;
                buff.Install(Target);
            }
        }

        public virtual void UnInstall(int id)
        {
            if (this.Remove(id, out IBuff<TTarget> buff))
            {
                buff.UnInstall(Target);
            }
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