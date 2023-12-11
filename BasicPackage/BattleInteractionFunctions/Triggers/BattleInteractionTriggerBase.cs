using System;
using System.Collections.Generic;
using InfinityWorldChess.BattleBuffDomain;
using InfinityWorldChess.BattleBuffFunction;
using InfinityWorldChess.BattleDomain;
using InfinityWorldChess.BattleInteractionDomain;
using InfinityWorldChess.BuffDomain;
using InfinityWorldChess.FunctionDomain;
using InfinityWorldChess.Ugf;
using Secyud.Ugf;
using Secyud.Ugf.DataManager;
using UnityEngine;

namespace InfinityWorldChess.BattleInteractionFunctions
{
    public abstract class BattleInteractionTriggerBase : IBuffEffect, IPropertyAttached,
        IActionable<BattleInteraction>, IHasContent, ITriggerable,IBuffAttached
    {
        private IAttachProperty _property;
        [field: S] public byte InteractionType { get; set; }

        public IAttachProperty Property
        {
            get => _property;
            set
            {
                _property = value;
                Attached(value);
            }
        }

        protected HashSet<IActionable<BattleInteraction>> Container { get; set; }

        public event Action ExtraActions;

        public virtual void Invoke(BattleInteraction interaction)
        {
            ExtraActions?.Invoke();
        }

        public virtual void Attached(IAttachProperty property)
        {
        }

        public virtual void InstallFrom(BattleUnit target)
        {
            BattleEvents events = target.Properties.GetOrCreate<BattleEvents>();
            Container = InteractionType switch
            {
                0 => events.PrepareLaunch,
                1 => events.PrepareReceive,
                2 => events.LaunchCallback,
                3 => events.ReceiveCallback,
                _ => events.PrepareLaunch
            };
            Container?.Add(this);
        }

        public virtual void UnInstallFrom(BattleUnit target)
        {
            Container?.Remove(this);
        }

        public virtual void Overlay(IOverlayable<BattleUnit> otherOverlayable)
        {
        }

        public virtual void SetContent(Transform transform)
        {
            string text = InteractionType switch
            {
                0 => "发动技能前",
                1 => "受到技能前",
                2 => "发动技能后",
                3 => "受到技能后",
                _ => "发动技能前"
            };

            transform.AddParagraph("每次受到技能触发。");
        }

        public IBattleUnitBuff Buff { get => null; set=> SetBuff(value); }

        protected virtual void SetBuff(IBattleUnitBuff buff)
        {
            
        }
    }
}