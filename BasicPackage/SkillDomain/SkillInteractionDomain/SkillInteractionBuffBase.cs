#region

using InfinityWorldChess.BuffDomain;

#endregion

namespace InfinityWorldChess.SkillDomain.SkillInteractionDomain
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

		public abstract int Id { get; }
	}
}