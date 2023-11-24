using System;
using InfinityWorldChess.BattleBuffDomain;
using InfinityWorldChess.BattleDomain;
using InfinityWorldChess.BuffDomain;
using InfinityWorldChess.FunctionDomain;
using InfinityWorldChess.Ugf;
using Secyud.Ugf;
using Secyud.Ugf.DataManager;
using UnityEngine;

namespace InfinityWorldChess.BattleBuffFunction
{
    /// <summary>
    /// provide trigger in round calculate.
    /// </summary>
    public class RoundCalculateTrigger :
        IBuffEffect, IPropertyAttached, IActionable, IHasContent,ITriggerable
    {
        private BattleContext Context => BattleScope.Instance.Context;
        
        [field: S] public IActionable Actionable { get; set; }
        [field: S] public ILimitable Limit { get; set; }
        [field: S(0)] public int Time { get; set; }

        public IAttachProperty Property { get; set; }

        public event Action ExtraActions;
        protected float TimeRecord { get; set; }

        public void Invoke()
        {
            float currentTime = Context.TotalTime;
            while (currentTime > TimeRecord + Time)
            {
                Property.Attach(Limit);
                Property.Attach(Actionable);
            
                if (Limit is null || Limit.CheckUseful())
                {
                    Actionable?.Invoke();
                    ExtraActions?.Invoke();
                }

                TimeRecord += Time;
            }
        }
        
        public void Install(BattleRole target)
        {
            TimeRecord = Context.TotalTime;
            Context.RoundBeginAction += Invoke;
        }

        public void UnInstall(BattleRole target)
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