using System;
using System.Collections.Generic;
using Secyud.Ugf.DependencyInjection;

namespace InfinityWorldChess.BattleDomain
{
    [Registry(DependScope = typeof(BattleScope))]
    public class BattleContext:IRegistry
    {
        public float TotalTime { get; set; }
        
        
        private int _idRecord;
        public readonly Dictionary<int, BattleRole> Roles = new();
        public int GetNextId => _idRecord++;
        
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