using System;
using InfinityWorldChess.BattleBuffDomain;
using InfinityWorldChess.BattleBuffFunction;
using InfinityWorldChess.BattleDomain;
using InfinityWorldChess.BuffDomain;
using InfinityWorldChess.FunctionDomain;
using InfinityWorldChess.Ugf;
using Secyud.Ugf;
using Secyud.Ugf.DataManager;
using UnityEngine;

namespace InfinityWorldChess.BattleInteractionFunctions
{
    /// <summary>
    /// provide trigger in round calculate.
    /// </summary>
    public class TurnCalculateTrigger :
        IBuffEffect, IPropertyAttached, IActionable, IHasContent,ITriggerable,IBuffAttached
    {
        private BattleContext Context => BattleScope.Instance.Context;
        
        [field: S] public IActionable Actionable { get; set; }
        [field: S] public ILimitable Limit { get; set; }
        [field: S(0)] public int Turn { get; set; }

        public int TurnRecord { get; set; }
        public event Action ExtraActions;
        public IAttachProperty Property
        {
            get => null;
            set
            {
                value.TryAttach(Limit);
                value.TryAttach(Actionable);
            }
        }
        public IBattleUnitBuff Buff
        {
            get => null;
            set
            {
                value.TryAttach(Limit);
                value.TryAttach(Actionable);
            }
        }
        public void Invoke()
        {
            TurnRecord += 1;
            if (TurnRecord <= Turn)
                return;
            
            TurnRecord = 0;
            
            if (Limit is null || Limit.CheckUseful())
            {
                Actionable?.Invoke();
                ExtraActions?.Invoke();
            }
        }
        
        public void InstallFrom(BattleUnit target)
        {
            TurnRecord = 0;
            Context.RoundBeginAction += Invoke;
        }

        public void UnInstallFrom(BattleUnit target)
        {
            Context.RoundBeginAction -= Invoke;
        }
        
        public void SetContent(Transform transform)
        {
            transform.AddParagraph($"每{Turn}回合触发。");
            Limit.TrySetContent(transform);
            Actionable.TrySetContent(transform);
        }

        public void Overlay(IOverlayable<BattleUnit> otherOverlayable)
        {
            
        }
    }
}