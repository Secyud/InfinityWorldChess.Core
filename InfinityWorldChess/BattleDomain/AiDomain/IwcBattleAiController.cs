﻿using System.Collections;
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
        private readonly List<IAiActionNode> _nodes;
        private IAiActionNode _currentNode;

        public static int TargetDistance { get; set; }

        public IwcBattleAiController(BattleControlService service, BattleContext context)
        {
            _service = service;
            _context = context;
            _nodes = new List<IAiActionNode>();
        }

        public void TryPondering()
        {
            if (_currentNode is not null &&
                _currentNode.IsInterval)
            {
                return;
            }

            BattleRole battleRole = _context.Unit;

            if (battleRole is null)
            {
                _service.ExitControl();
                return;
            }

            _nodes.Clear();
            SkillAiActionNode.AddNodes(_nodes, battleRole);
            MoveAiActionNode.AddNodes(_nodes, battleRole);
            _nodes.Add(new StopActionNode());

            _currentNode = RandomSelectNode();

            if (_currentNode is null || !_currentNode.InvokeAction())
            {
                _service.ExitControl();
            }
        }


        private IAiActionNode RandomSelectNode()
            {
                int count = _nodes.Count;
                if (count <= 0) return null;

                int[] steps = new int[count];
                steps[0] = _nodes[0].GetScore();
                for (int i = 1; i < count; i++)
                {
                    steps[i] = steps[i - 1] + _nodes[i].GetScore();
                }

                int total = steps[^1];

                if (total <= 0) return null;

                int rd = U.GetRandom(total);

                for (int i = 0; i < count; i++)
                {
                    if (steps[i] > rd)
                    {
                        return _nodes[i];
                    }
                }

                return null;
            }
        }
    }