#region

using System;
using System.Collections.Generic;

#endregion

namespace InfinityWorldChess.BuffDomain
{
	public abstract class FixTargetBuffProperty<TTarget>
	{
		public BuffProperty<TTarget> Buffs { get; } = new();

		protected abstract TTarget Target { get; }

		public TBuff Get<TBuff>() where TBuff : class, IBuff<TTarget>
		{
			return Buffs.Get<TBuff>();
		}

		public void Install<TBuff>(TBuff buff) where TBuff : class, IBuff<TTarget>
		{
			Buffs.Install(buff, Target);
		}

		public TBuff GetOrInstall<TBuff>() where TBuff : class, IBuff<TTarget>
		{
			return Buffs.GetOrInstall<TBuff>(Target);
		}

		public void UnInstall<TBuff>() where TBuff : class, IBuff<TTarget>
		{
			Buffs.UnInstall<TBuff>(Target);
		}

		public void UnInstall(Type buffType)
		{
			Buffs.UnInstall(Target, buffType);
		}

		public bool Contains<TBuff>() where TBuff : class, IBuff<TTarget>
		{
			return Buffs.Contains<TBuff>();
		}

		public List<IBuffCanBeShown<TTarget>> GetVisibleBuff()
		{
			return Buffs.GetVisibleBuff();
		}
	}
}