using InfinityWorldChess.RoleDomain;
using InfinityWorldChess.SkillDomain;
using JetBrains.Annotations;
using Secyud.Ugf.HexMap;
using System;
using System.Collections.Generic;
using System.Linq;

namespace InfinityWorldChess.BattleDomain
{
	public class CoreSkillAiAction : AiActionNode
	{
		private readonly HexCell _cell;
		private readonly CoreSkillContainer _container;
		private readonly RoleBattleChess _role;
		private readonly BattleContext _context;

		private CoreSkillAiAction([NotNull] HexCell cell,
			[NotNull] CoreSkillContainer container,
			[NotNull] RoleBattleChess role,
			[NotNull]BattleContext context)
		{
			_cell = cell;
			_container = container;
			_role = role;
			_context = context;
		}

		public override void InvokeAction()
		{
			_context.CurrentSkill = _container;
			_context.SetHoverRange(_context.GetChecker(_cell) );
			_context.StartCurrentSkillCast(_cell);
		}

		public override int GetScore()
		{
			Role.NatureProperty n = _role.Role.Nature;
			float score = n.Confident * (_container.Skill.Score - 128);

			ISkillRange range = _container.Skill.GetCastResultRange(_role, _cell);
			ISkillTarget skillTarget = _container.CoreSkill.GetTargetInRange(_role, range);

			if (skillTarget.Value.IsNullOrEmpty())
			{
				float minDistance = float.MaxValue;
				IBattleChess chess = null;
				foreach (IBattleChess c in _context.Chesses.Values)
				{
					if (c.Camp != _role.Camp)
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

				int origin = (int)_role.Direction;
				int current = (int)_cell.DirectionTo(_role.Unit.Location);
				int target = (int)chess.Unit.Location.DirectionTo(_role.Unit.Location);
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

		public static void AddNodes(List<AiActionNode> nodes, BattleContext context, RoleBattleChess role)
		{
			foreach (CoreSkillContainer skill in context.CurrentRole.NextCoreSkills)
			{
				if (skill is not null &&
					skill.CoreSkill.CheckCastCondition(role) is null)
				{
					nodes.AddRange(
						skill.CoreSkill.GetCastPositionRange(role).Value
							.Select(cell => new CoreSkillAiAction(cell, skill, role,context))
					);
				}
			}
		}
	}
}