using System.Collections;
using Secyud.Ugf;
using Secyud.Ugf.DependencyInjection;
using System.Collections.Generic;

namespace InfinityWorldChess.BattleDomain
{
	public class IwcBattleAiController : IBattleAiController, IScoped
	{
		public AiActionNode ResultNode { get; private set;  }
		public AiControlState State { get; private set; }

		private readonly BattleContext _context;
		public IwcBattleAiController(BattleContext context)
		{
			_context = context;
		}
		
		public IEnumerator StartPondering()
		{
			State = AiControlState.InPondering;
			List<AiActionNode> nodes = new();
			RoleBattleChess role = _context.CurrentRole;
			if (role is null)
				State = AiControlState.NoActionValid;
			else
			{
				CoreSkillAiAction.AddNodes(nodes, _context, role);
				FormSkillAiAction.AddNodes(nodes, _context, role);
				yield return RandomSelect(nodes);
			}
			State = AiControlState.FinishPonder;
		}

		public void TryInvokeCurrentNode()
		{
			if (State == AiControlState.NoActionValid)
				return;
			ResultNode?.InvokeAction();
			ResultNode = null;
			State = AiControlState.StartPonder;
		}


		public IEnumerator RandomSelect(List<AiActionNode> nodes)
		{
			int count = nodes.Count;
			if (count <= 0)
				State = AiControlState.NoActionValid;
			else
			{
				int[] steps = new int[count];
				steps[0] = nodes[0].GetScore();
				for (int i = 1; i < count; i++)
				{
					yield return null;
					steps[i] = steps[i - 1] + nodes[i].GetScore();
				}

				int total = steps[^1];

				if (total <= 0)
					State = AiControlState.NoActionValid;
				else
				{
					int rd = Og.GetRandom(total);

					for (int i = 0; i < count; i++)
						if (steps[i] > rd)
						{
							ResultNode = nodes[i];
							break;
						}
				}
			}
		}
	}
}