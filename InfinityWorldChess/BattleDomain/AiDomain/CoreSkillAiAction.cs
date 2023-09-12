using System;
using System.Collections.Generic;
using System.Linq;
using InfinityWorldChess.BattleDomain.BattleMapDomain;
using InfinityWorldChess.BattleDomain.BattleSkillDomain;
using InfinityWorldChess.RoleDomain;
using InfinityWorldChess.SkillDomain;
using JetBrains.Annotations;
using Secyud.Ugf;
using Secyud.Ugf.HexMap;

namespace InfinityWorldChess.BattleDomain.AiDomain
{
	public class CoreSkillAiAction : AiActionNode
	{
		private readonly HexCell _cell;
		private readonly CoreSkillContainer _container;
		private readonly BattleRole _battleRole;

		private CoreSkillAiAction([NotNull] HexCell cell,
			[NotNull] CoreSkillContainer container,
			[NotNull] BattleRole battleRole)
		{
			_cell = cell;
			_container = container;
			_battleRole = battleRole;
		}

		public override void InvokeAction()
		{
			SkillObservedService skill = U.Get<SkillObservedService>();
			skill.Skill = _container;
			BattleMap map = BattleScope.Instance.Map;
			map.StartCurrentSkillCast(_cell);
		}

		public override int GetScore()
		{
			Role.NatureProperty n = _battleRole.Role.Nature;
			float score = n.Confident * (_container.Skill.Score - 128);

			ISkillRange range = _container.Skill.GetCastResultRange(_battleRole, _cell);
			return Math.Max((int)(score / 512 + 16), 1);
		}

		public static void AddNodes(List<AiActionNode> nodes, BattleRole battleRole)
		{
			foreach (CoreSkillContainer skill in battleRole.NextCoreSkills)
			{
				if (skill is not null &&
					skill.CoreSkill.CheckCastCondition(battleRole) is null)
				{
					nodes.AddRange(
						skill.CoreSkill.GetCastPositionRange(battleRole).Value
							.Select(cell => new CoreSkillAiAction(cell, skill, battleRole))
					);
				}
			}
		}
	}
}