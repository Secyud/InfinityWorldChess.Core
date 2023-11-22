using InfinityWorldChess.SkillDomain;
using InfinityWorldChess.Ugf;
using Secyud.Ugf.DataManager;
using Secyud.Ugf.HexMap;
using UnityEngine;

namespace InfinityWorldChess.SkillFunctions
{
    public class DistanceAddDamage : ISkillInteractionEffect
    {
        [field: S] public float Factor { get; set; }

        public ActiveSkillBase BelongSkill { get; set; }

        public void SetContent(Transform transform)
        {
            transform.AddParagraph($"每单位距离增加此招式{Factor:P0}伤害。");
        }
        public void Invoke(SkillInteraction interaction)
        {
            HexUnit attackerUnit = interaction.LaunchChess.Unit;
            HexUnit defenderUnit = interaction.TargetChess.Unit;
            AttackRecordBuff attackRecord = interaction.GetOrAddAttack();
            attackRecord.DamageFactor += Factor * attackerUnit.DistanceTo(defenderUnit);
        }
    }
}