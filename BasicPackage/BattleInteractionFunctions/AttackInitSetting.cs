using System;
using System.Runtime.InteropServices;
using InfinityWorldChess.BattleInteractionDomain;
using InfinityWorldChess.FunctionDomain;
using InfinityWorldChess.GlobalDomain;
using InfinityWorldChess.SkillDomain;
using InfinityWorldChess.Ugf;
using Secyud.Ugf;
using Secyud.Ugf.DataManager;
using UnityEngine;

namespace InfinityWorldChess.BattleInteractionFunctions
{
    [Guid("CCE647E1-87CE-5724-31FE-9A8DD4D1964C")]
    public class AttackInitSetting:IHasPriority,
        IActionable<BattleInteraction>,IHasContent
    {
        [field: S] public float Attack { get; set; }
        [field: S] public byte AttackType { get; set; }
        [field: S] public float Penetration { get; set; }
        [field: S] public float AttackFixedValue { get; set; }
        [field: S] public float AttackFactor { get; set; } = 1f;
        [field: S] public float Defend { get; set; }
        [field: S] public float DamageFactor { get; set; } = 1f;
        [field: S] public float DefendFactor { get; set; }

        public int Priority => -0x10001;

        
        public void SetContent(Transform transform)
        {
            string str = "";

            if (Attack != 0)
            {
                str += "\r\n攻击: " + Attack;
            }

            if (Defend != 0)
            {
                str += "\r\n防御削减: " + -Defend;
            }

            if (AttackType != 0)
            {
                str += "\r\n攻击类型: " + ((AttackType)AttackType) switch
                {
                    SkillDomain.AttackType.Magical  => "内伤",
                    SkillDomain.AttackType.Physical => "外伤",
                    SkillDomain.AttackType.Mental   => "精神",
                    _                               => "未知"
                };
            }

            if (Penetration != 0)
            {
                str += "\r\n护甲穿透: " + Penetration;
            }

            if (AttackFixedValue != 0)
            {
                str += "\r\n额外攻击: " + AttackFixedValue;
            }

            if (Math.Abs(AttackFactor - 1) > IWCC.T)
            {
                str += "\r\n攻击倍率: " + AttackFactor;
            }

            if (Math.Abs(DamageFactor - 1) > IWCC.T)
            {
                str += "\r\n伤害倍率: " + DamageFactor;
            }

            if (DefendFactor != 0)
            {
                str += "\r\n伤害减免: " + DefendFactor;
            }

            transform.AddParagraph(str);
        }

        public void Invoke(BattleInteraction interaction)
        {
            AttackRecordProperty attackRecord = interaction.GetOrAddAttack();
            attackRecord.Attack = Attack;
            attackRecord.AttackType = (AttackType)AttackType;
            attackRecord.Penetration = Penetration;
            attackRecord.AttackFixedValue = AttackFixedValue;
            attackRecord.AttackFactor = AttackFactor;
            attackRecord.Defend = Defend;
            attackRecord.DamageFactor = DamageFactor;
            attackRecord.DefendFactor = DefendFactor;
        }
    }
}