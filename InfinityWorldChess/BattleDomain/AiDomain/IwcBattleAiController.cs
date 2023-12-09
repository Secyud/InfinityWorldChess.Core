using System.Collections;
using System.Collections.Generic;
using Secyud.Ugf;
using Secyud.Ugf.DependencyInjection;

namespace InfinityWorldChess.BattleDomain
{
    [Registry(DependScope = typeof(BattleScope))]
    public class IwcBattleAiController : IBattleAiController
    {
        private readonly BattleControlService _service;
        private readonly BattleContext _context;
        
        public static int TargetDistance { get; set; }

        public IwcBattleAiController(BattleControlService service, BattleContext context)
        {
            _service = service;
            _context = context;
        }

        public IEnumerator StartPondering()
        {
            AiActionNode node = null;

            do
            {
                if (_context.State == BattleFlowState.OnUnitControl)
                {
                    BattleRole battleRole = BattleScope.Instance.Context.Role;
                    List<AiActionNode> nodes = new();
                    if (battleRole is null)
                        _service.ExitControl();
                    else
                    {
                        SkillAiActionNode.AddNodes(nodes, battleRole);
                        MoveAiActionNode.AddNodes(nodes, battleRole);
                        nodes.Add(new StopActionNode());
                        node = RandomSelect(nodes);
                        node?.InvokeAction();
                    }
                }
                yield return null;
            } while (node is not null);

            _service.ExitControl();
        }


        public AiActionNode RandomSelect(List<AiActionNode> nodes)
        {
            int count = nodes.Count;
            if (count <= 0) return null;

            int[] steps = new int[count];
            steps[0] = nodes[0].GetScore();
            for (int i = 1; i < count; i++)
            {
                steps[i] = steps[i - 1] + nodes[i].GetScore();
            }

            int total = steps[^1];

            if (total <= 0) return null;

            int rd = U.GetRandom(total);

            for (int i = 0; i < count; i++)
            {
                if (steps[i] > rd)
                {
                    return nodes[i];
                }
            }

            return null;
        }
    }
}