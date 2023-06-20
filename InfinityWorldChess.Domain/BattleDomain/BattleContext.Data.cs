using System;
using System.Collections.Generic;

namespace InfinityWorldChess.BattleDomain
{
    public partial class BattleContext
    {
        private int _idRecord;
        public readonly Dictionary<int, IBattleChess> Chesses = new();
        public readonly List<RoleBattleChess> Roles = new();
        public BattleChecker[] Checkers;
        public int GetNextId => _idRecord++;
        public Battle Battle { get; private set; }
        public float TotalTime { get; private set; }
        public int Width { get; private set; }
        public int Height { get; private set; }
        public int CellCount { get; private set; }
        public BattleFlowState State { get; private set; }
        public bool PlayerControl => CurrentRole.PlayerControl;

        public event Action RoundBeginAction;

        public event Action RoundEndAction;

        public event Action ChessRemoveAction;

        public void OnRoundEnd()
        {
            RoundEndAction?.Invoke();
        }

        public void OnRoundBegin()
        {
            RoundBeginAction?.Invoke();
        }

        public void OnChessRemove()
        {
            ChessRemoveAction?.Invoke();
        }
    }
}