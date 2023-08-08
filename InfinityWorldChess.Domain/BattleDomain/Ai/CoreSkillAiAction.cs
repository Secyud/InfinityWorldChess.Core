using InfinityWorldChess.RoleDomain;
using InfinityWorldChess.SkillDomain;
using JetBrains.Annotations;
using Secyud.Ugf.HexMap;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Ugf.Collections.Generic;
using InfinityWorldChess.BattleDomain.BattleMapDomain;
using InfinityWorldChess.BattleDomain.BattleSkillDomain;
using Secyud.Ugf;

namespace InfinityWorldChess.BattleDomain
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
			var skill = U.Get<SkillRefreshService>();
			skill.Skill = _container;
			BattleMap map = BattleScope.Instance.Map;
			map.StartCurrentSkillCast(_cell);
		}

		public override int GetScore()
		{
			Role.NatureProperty n = _battleRole.Role.Nature;
			float score = n.Confident * (_container.Skill.Score - 128);

			ISkillRange range = _container.Skill.GetCastResultRange(_battleRole, _cell);
			ISkillTarget skillTarget = _container.CoreSkill.GetTargetInRange(_battleRole, range);

			if (skillTarget.Value.IsNullOrEmpty())
			{
				float minDistance = float.MaxValue;
				BattleRole chess = null;
				foreach (BattleRole c in BattleScope.Instance.Context.Roles.Values)
				{
					if (c.Camp != _battleRole.Camp)
					{
						float distance = c.Unit.Location.Coordinates.DistanceTo(_cell.Coordinates);

						if (minDistance > distance)
						{
							minDistance = distance;
							chess = c;
						}
					}
				}
				if (chess is null)
					return 0;

				int origin = (int)_battleRole.Direction;
				int current = (int)_cell.DirectionTo(_battleRole.Unit.Location);
				int target = (int)chess.Unit.Location.DirectionTo(_battleRole.Unit.Location);
				if (Math.Abs(target - current) >= Math.Abs(target - origin))
					return 0;
				else
					return 1;
			}

			SkillTargetType targetType = _container.Skill.TargetType;
			score += skillTarget.Value.Length * n.Recognize;

			if ((targetType & SkillTargetType.Self) > 0)
			{
				if (_container.Skill.Damage)
					score += -n.Confident - n.Recognize - n.Stability;
				else
					score += n.Confident + n.Recognize + n.Stability;
			}
			if ((targetType & SkillTargetType.Enemy) > 0)
			{
				if (_container.Skill.Damage)
					score += n.Confident + n.Recognize - n.Stability;
				else
					score += -n.Confident - n.Recognize + n.Stability;
			}
			if ((targetType & SkillTargetType.Teammate) > 0)
			{
				if (_container.Skill.Damage)
					score += n.Confident - n.Altruistic - n.Stability;
				else
					score += -n.Confident + n.Altruistic + n.Stability;
			}
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