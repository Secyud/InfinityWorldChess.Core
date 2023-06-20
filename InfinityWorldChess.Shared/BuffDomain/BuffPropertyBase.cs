#region

using Secyud.Ugf.TypeHandle;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Secyud.Ugf.Archiving;

#endregion

namespace InfinityWorldChess.BuffDomain
{
	public class BuffPropertyBase<TTarget, TAbstractBuff> : SortedDictionary<TypeStruct, TAbstractBuff>
		where TAbstractBuff : class, IBuff<TTarget>
	{
		public virtual TBuff Get<TBuff>() where TBuff : class, IBuff<TTarget>
		{
			return TryGetValue(typeof(TBuff).Describe(), out TAbstractBuff buff) ? buff as TBuff : null;
		}

		public virtual void Install<TBuff>(TBuff buff, TTarget target) where TBuff : class, TAbstractBuff
		{
			TryGetValue(typeof(TBuff).Describe(), out TAbstractBuff oBuff);
			if (oBuff is null)
			{
				this[typeof(TBuff).Describe()] = buff;
				buff.Install(target);
			}
			else
			{
				oBuff.Overlay(buff);
			}
		}

		public virtual TBuff GetOrInstall<TBuff>(TTarget target) where TBuff : class, TAbstractBuff
		{
			TryGetValue(typeof(TBuff).Describe(), out TAbstractBuff oBuff);
			if (oBuff is not null) return oBuff as TBuff;

			TBuff buff = typeof(TBuff).GetConstructor(Type.EmptyTypes)!
				.Invoke(Array.Empty<object>()) as TBuff;
			this[typeof(TBuff).Describe()] = buff;
			buff!.Install(target);
			return buff;
		}

		public virtual void UnInstall<TBuff>(TTarget target) where TBuff : class, IBuff<TTarget>
		{
			UnInstall(target, typeof(TBuff));
		}

		public virtual void UnInstall(TTarget target, Type buffType)
		{
			if (this.Remove(buffType.Describe(), out TAbstractBuff oBuff)) oBuff.UnInstall(target);
		}

		public bool Contains<TBuff>() where TBuff : class, IBuff<TTarget>
		{
			return ContainsKey(typeof(TBuff).Describe());
		}

		public List<IBuffCanBeShown<TTarget>> GetVisibleBuff()
		{
			return Values
				.Where(u => u is IBuffCanBeShown<TTarget> { Visible: true })
				.Cast<IBuffCanBeShown<TTarget>>()
				.ToList();
		}
		
		

		public virtual void Save(IArchiveWriter writer)
		{
			TAbstractBuff[] buffs = Values
				.Where(b => b is IArchivable)
				.ToArray();

			writer.Write(buffs.Length);
			foreach (TAbstractBuff buff in buffs)
				writer.Write(buff);
		}

		public virtual void Load(IArchiveReader reader, TTarget target)
		{
			int count = reader.ReadInt32();
			for (int i = 0; i < count; i++)
			{
				TAbstractBuff buff = reader.Read<TAbstractBuff>();
				Install(buff, target);
			}
		}

		public void Clear(TTarget target)
		{
			foreach (TAbstractBuff buff in Values)
				buff.UnInstall(target);
			Clear();
		}
	}
}