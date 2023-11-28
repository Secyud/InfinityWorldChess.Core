﻿using InfinityWorldChess.BattleBuffFunction;
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
    public class LimitInteractionActionableFull : IBuffAttached, IPropertyAttached, IHasContent,
        IActionable<BattleInteraction>
    {
        [field: S] public IActionable<BattleInteraction> Actionable { get; set; }
        [field: S] public ILimitable<BattleInteraction> Limitable { get; set; }

        public IBattleRoleBuff Buff
        {
            get => null;
            set
            {
                value.TryAttach(Actionable);
                value.TryAttach(Limitable);
            }
        }

        public IAttachProperty Property
        {
            get => null;
            set
            {
                value.TryAttach(Actionable);
                value.TryAttach(Limitable);
            }
        }

        public void SetContent(Transform transform)
        {
            Limitable.TrySetContent(transform);
            Actionable.TrySetContent(transform);
        }

        public void Invoke(BattleInteraction target)
        {
            if (Limitable is null || Limitable.CheckUseful(target))
            {
                Actionable?.Invoke(target);
            }
        }
    }
}