﻿using System.Collections;
using System.Collections.Generic;
using InfinityWorldChess.BattleDomain.BattleRoleDomain;
using Secyud.Ugf;
using Secyud.Ugf.DependencyInjection;

namespace InfinityWorldChess.BattleDomain
{
    [Registry(DependScope = typeof(BattleScope))]
    public class IwcBattleAiController : IBattleAiController
    {
        public AiActionNode ResultNode { get; private set; }
        public AiControlState State { get; private set; }

        public IEnumerator StartPondering()
        {
            State = AiControlState.InPondering;
            List<AiActionNode> nodes = new();
            BattleRole battleRole = U.Get<RoleRefreshService>().Role;
            if (battleRole is null)
                State = AiControlState.NoActionValid;
            else
            {
                CoreSkillAiAction.AddNodes(nodes, battleRole);
                FormSkillAiAction.AddNodes(nodes, battleRole);
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
                    int rd = U.GetRandom(total);

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