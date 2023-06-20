﻿namespace InfinityWorldChess.GlobalDomain
{
	public interface IActionable<in TTarget>
	{
		public int Priority { get; }

		void Active(TTarget target);
	}
}