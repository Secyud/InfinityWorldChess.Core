﻿using InfinityWorldChess.BattleDomain;
using InfinityWorldChess.SkillDomain;
using InfinityWorldChess.Ugf;
using Secyud.Ugf.HexMap;
using Secyud.Ugf.HexUtilities;
using UnityEngine;

namespace InfinityWorldChess.SkillFunctions
{
    /// <summary>
    /// 击退一格，若失败，则再攻击一次
    /// </summary>
    public class RepelOrAttackMore : ISkillInteractionEffect
    {
        public ActiveSkillBase BelongSkill { get; set; }
        public void SetContent(Transform transform)
        {
            transform.AddParagraph("击退敌方一格，若因阻挡而无法击退，则再次对敌方造成伤害。");
        }

        public void Invoke(SkillInteraction interaction)
        {
            HexCell lC = interaction.LaunchChess.Unit.Location;
            HexCell tC = interaction.TargetChess.Unit.Location;
            HexDirection direction = lC.DirectionTo(tC);
            HexCell neighbour = tC.GetNeighbor(direction);
            if (neighbour && !neighbour.Unit)
            {
                interaction.TargetChess.Unit.Location = neighbour;
            }
            else if (interaction.TargetChess is ICanDefend defender)
            {
                AttackRecordBuff attackRecord = interaction.GetOrAddAttack();
                attackRecord.RunDamage(defender);
            }
        }
    }
}