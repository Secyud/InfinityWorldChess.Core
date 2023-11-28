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
    [ID("69DD7F7A-1E43-180F-C4C2-41A2C1761F6E")]
    public class RoundCalculateTrigger :
        IBuffEffect, IPropertyAttached, IActionable, IHasContent,ITriggerable,IBuffAttached
    {
        private BattleContext Context => BattleScope.Instance.Context;
        
        [field: S] public IActionable Actionable { get; set; }
        [field: S] public ILimitable Limit { get; set; }
        [field: S(0)] public int Time { get; set; }

        public event Action ExtraActions;
        protected float TimeRecord { get; set; }
        
        public IAttachProperty Property
        {
            get => null;
            set
            {
                value.TryAttach(Limit);
                value.TryAttach(Actionable);
            }
        }

        public IBattleRoleBuff Buff
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
            float currentTime = Context.TotalTime;
            while (currentTime > TimeRecord + Time)
            {
            
                if (Limit is null || Limit.CheckUseful())
                {
                    Actionable?.Invoke();
                    ExtraActions?.Invoke();
                }

                TimeRecord += Time;
            }
        }
        
        public void InstallFrom(BattleRole target)
        {
            TimeRecord = Context.TotalTime;
            Context.RoundBeginAction += Invoke;
        }

        public void UnInstallFrom(BattleRole target)
        {
            Context.RoundBeginAction -= Invoke;
        }
        
        public void SetContent(Transform transform)
        {
            transform.AddParagraph($"每{Time}时序触发。");
            Limit.TrySetContent(transform);
            Actionable.TrySetContent(transform);
        }

        public void Overlay(IOverlayable<BattleRole> otherOverlayable)
        {
            
        }
    }
}