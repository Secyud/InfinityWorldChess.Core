#region

using Secyud.Ugf.TypeHandle;
using System.Collections.Generic;

#endregion

namespace InfinityWorldChess.BuffDomain
{
	public class ParasitiferProperty<TTarget> :
		BuffPropertyBase<TTarget, IParasiteBuff<TTarget>>, IParasitifer<TTarget>
		where TTarget : class, IParasitifer<TTarget>
	{
		public SortedDictionary<TypeDescriptor, IParasiteBuff<TTarget>> Parasites => this;

		public override void Install<TBuff>(TBuff buff, TTarget target)
		{
			base.Install(buff, target);
			if (buff is not null)
				buff.Parasitifer = this as TTarget;
		}

		public override TBuff GetOrInstall<TBuff>(TTarget target)
		{
			TBuff buff = base.GetOrInstall<TBuff>(target);
			if (buff is not null)
				buff.Parasitifer = this as TTarget;
			return buff;
		}
	}
}