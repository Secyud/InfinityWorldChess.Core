﻿using InfinityWorldChess.BattleDomain;
using InfinityWorldChess.BattleInteractionDomain;
using InfinityWorldChess.BuffDomain;
using InfinityWorldChess.FunctionDomain;
using InfinityWorldChess.Ugf;
using Secyud.Ugf;
using Secyud.Ugf.DataManager;
using UnityEngine;

namespace InfinityWorldChess.BattleInteractionFunctions
{
    /// <summary>
    /// 延缓敌方行动时间
    /// </summary>
    public class AddTargetTime : IActionable<BattleInteraction>,IHasContent,IPropertyAttached
    {
        [field: S] public float Factor { get; set; }
        [field: S] public float Value { get; set; }
        public IAttachProperty Property { get; set; }
        
        public void SetContent(Transform transform)
        {
            transform.AddParagraph($"此招式延缓敌方{Value}+{Factor:P0}[灵]点时序。");
        }

        public void Invoke(BattleInteraction interaction)
        {
            if (Property is not null)
            {
                BattleRole chess = interaction.Target;
                chess.Time += (int)(Value + Property.Nimble * Factor);
            }
        }
    }
}