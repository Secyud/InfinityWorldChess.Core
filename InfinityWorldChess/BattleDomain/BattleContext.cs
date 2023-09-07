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
        
        public event Action BattleFinishAction;

        public void OnBattleFinished()
        {
            BattleFinishAction?.Invoke();
        }

        public event Action RoundEndAction;
        public void OnRoundEnd()
        {
            RoundEndAction?.Invoke();
        }

        public event Action RoundBeginAction;
        public void OnRoundBegin()
        {
            RoundBeginAction?.Invoke();
        }

        public event Action ChessRemoveAction;
        public void OnChessRemove()
        {
            ChessRemoveAction?.Invoke();
        }
    }
}