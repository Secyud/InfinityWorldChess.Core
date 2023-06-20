#region

using System;
using InfinityWorldChess.BattleDomain;
using InfinityWorldChess.SkillDomain;
using Secyud.Ugf.HexMap;

#endregion

namespace InfinityWorldChess.BasicBundle
{
	public abstract class SkillWithFlowBase : ActiveSkillBase
	{
		protected ISkillTarget Targets { get; private set; }

		public override void Release()
		{
			base.Release();
			Targets = null;
		}

		public override void Cast(IBattleChess battleChess, HexCell releasePosition)
		{
			base.Cast(battleChess,releasePosition);
			Targets = GetTargetInRange(
				battleChess,
				GetCastResultRange(battleChess, releasePosition)
			);
			PreSkill(battleChess, releasePosition);
			foreach (IBattleChess enemy in Targets.Value)
			{
				if (enemy is not ICanDefend) continue;

				SkillInteraction interaction =
					SkillInteraction.Get(battleChess, enemy);
				PreInteraction(interaction);
				interaction.BeforeHit();
				OnInteraction(interaction);
				interaction.AfterHit();
				PostInteraction(interaction);
			}

			PostSkill(battleChess, releasePosition);
			Targets = null;
		}

		protected virtual void OnInteraction(SkillInteraction interaction)
		{
		}

		protected virtual void PreInteraction(SkillInteraction interaction)
		{
		}

		protected virtual void PreSkill(IBattleChess battleChess, HexCell releasePosition)
		{
		}

		protected virtual void PostSkill(IBattleChess battleChess, HexCell releasePosition)
		{
		}

		protected virtual void PostInteraction(SkillInteraction interaction)
		{
		}

		public ISkillTarget GetTargetInRange(IBattleChess battleChess, ISkillRange range)
		{
			return TargetType switch
			{
				SkillTargetType.None => SkillTarget.GetFixedTarget(),
				SkillTargetType.Self => SkillTarget.GetFixedTarget(battleChess),
				SkillTargetType.Enemy => SkillTarget.GetEnemies(battleChess.Camp, range),
				SkillTargetType.Teammate => SkillTarget.GetTeammates(battleChess.Camp, range),
				SkillTargetType.SelfAndTeammate => SkillTarget.SelfAndTeammates(battleChess.Belong, range),
				SkillTargetType.ExcludeSelf => SkillTarget.ExcludeSelf(range),
				SkillTargetType.SelfAndEnemy => SkillTarget.SelfAndEnemy(battleChess.Belong, range),
				SkillTargetType.All => SkillTarget.All(battleChess.Belong, range),
				_ => throw new ArgumentOutOfRangeException()
			};
		}
	}
}