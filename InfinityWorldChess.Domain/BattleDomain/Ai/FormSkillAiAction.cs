using InfinityWorldChess.RoleDomain;
using InfinityWorldChess.SkillDomain;
using JetBrains.Annotations;
using Secyud.Ugf.HexMap;
using System;
using System.Collections.Generic;
using System.Linq;

namespace InfinityWorldChess.BattleDomain
{
	public class FormSkillAiAction : AiActionNode
	{
		private readonly HexCell _cell;
		private readonly FormSkillContainer _container;
		private readonly RoleBattleChess _role;
		private readonly BattleContext _context;

		private FormSkillAiAction(
			[NotNull] HexCell cell,
			[NotNull] FormSkillContainer container,
			[NotNull] RoleBattleChess role,
			[NotNull] BattleContext context)
		{
			_cell = cell;
			_container = container;
			_role = role;
			_context = context;
		}

		public override void InvokeAction()
		{
			_context.CurrentSkill = _container;
			_context.StartCurrentSkillCast(_cell);
		}

		public override int GetScore()
		{
			Role.NatureProperty n = _role.Role.Nature;
			float minDistance = float.MaxValue;
			float score = 0;
			foreach (IBattleChess chess in _context.Chesses.Values)
			{
				float distance = chess.Unit.Location.Coordinates.DistanceTo(_cell.Coordinates);
				if (chess.Camp == _role.Camp)
					score += n.Gregarious / 512;
				else
				{
					if (minDistance > distance)
						minDistance = distance;
				}
			}
			return Math.Max((int)(score + 32 - minDistance), 1);
		}


		public static void AddNodes(List<AiActionNode> nodes, BattleContext context, RoleBattleChess role)
		{
			foreach (FormSkillContainer skill in context.CurrentRole.NextFormSkills)
			{
				if (skill is not null &&
					skill.FormSkill.CheckCastCondition(role) is null)
				{
					nodes.AddRange(
						skill.FormSkill.GetCastPositionRange(role).Value
							.Select(cell => new FormSkillAiAction(cell, skill, role, context))
					);
				}
			}
		}
	}
}