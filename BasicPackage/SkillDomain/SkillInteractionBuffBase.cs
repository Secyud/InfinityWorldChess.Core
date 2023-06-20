#region

using InfinityWorldChess.BuffDomain;

#endregion

namespace InfinityWorldChess.SkillDomain
{
	public abstract class SkillInteractionBuffBase : IBuff<SkillInteraction>
	{
		public virtual void Install(SkillInteraction target)
		{
		}

		public virtual void UnInstall(SkillInteraction target)
		{
		}

		public virtual void Overlay(IBuff<SkillInteraction> finishBuff)
		{
		}
	}
}