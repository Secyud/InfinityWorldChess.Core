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
	public class FormSkillAiAction : AiActionNode
	{
		private readonly HexCell _cell;
		private readonly FormSkillContainer _container;
		private readonly BattleRole _battleRole;

		private FormSkillAiAction(
			[NotNull] HexCell cell,
			[NotNull] FormSkillContainer container,
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
			float minDistance = float.MaxValue;
			float score = 0;
			foreach (BattleRole chess in BattleScope.Instance.Context.Roles)
			{
				float distance = chess.Unit.Location.Coordinates.DistanceTo(_cell.Coordinates);
				if (chess.Camp == _battleRole.Camp)
					score += n.Gregarious / 512;
				else
				{
					if (minDistance > distance)
						minDistance = distance;
				}
			}
			return Math.Max((int)(score + 32 - minDistance), 1);
		}


		public static void AddNodes(List<AiActionNode> nodes,  BattleRole battleRole)
		{
			foreach (FormSkillContainer skill in battleRole.NextFormSkills)
			{
				if (skill is not null &&
					skill.FormSkill.CheckCastCondition(battleRole) is null)
				{
					nodes.AddRange(
						skill.FormSkill.GetCastPositionRange(battleRole).Value
							.Select(cell => new FormSkillAiAction(cell, skill, battleRole))
					);
				}
			}
		}
	}
}