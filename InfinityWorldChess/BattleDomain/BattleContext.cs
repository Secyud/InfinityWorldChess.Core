using System;
using System.Collections.Generic;
using Secyud.Ugf.DependencyInjection;

namespace InfinityWorldChess.BattleDomain
{
    [Registry(DependScope = typeof(BattleScope))]
    public class BattleContext:IRegistry
    {
        public float TotalTime { get; set; }

        public IReadOnlyList<BattleRole> BattleRoles => Roles;
        
        internal readonly List<BattleRole> Roles = new();
        
        public event Action BattleFinishAction;
        public void OnBattleFinished()
        {
            BattleFinishAction?.Invoke();
        }
        
        public event Action ActionFinishedAction;
        public void OnActionFinished()
        {
            ActionFinishedAction?.Invoke();
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

        public event Action ChessRemovedAction;
        public void OnChessRemoved()
        {
            ChessRemovedAction?.Invoke();
        }
        public event Action ChessAddedAction;
        public void OnChessAdded()
        {
            ChessAddedAction?.Invoke();
        }
    }
}