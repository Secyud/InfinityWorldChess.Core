using System.Runtime.InteropServices;
using InfinityWorldChess.BattleDomain;
using InfinityWorldChess.Ugf;
using Secyud.Ugf.DataManager;
using UnityEngine;

namespace InfinityWorldChess.SkillDomain
{
    [Guid("AC08FF2D-703C-F428-F92E-581012FC2693")]
    public class SkillEnergyCast : ISkillCastCondition
    {
        [field: S] public float EnergyConsume { get; set; }
        [field: S] public int ExecutionConsume { get; set; }

        public string CheckCastCondition(BattleUnit chess, IActiveSkill skill)
        {
            if (chess.ExecutionValue < ExecutionConsume)
                return "行动力不足，无法释放技能。";


            if (chess.EnergyValue < GetEnergyConsume(skill))
                return "内力不足，无法释放技能。";

            return null;
        }

        public void ConditionCast(BattleUnit chess, IActiveSkill skill)
        {
            chess.ExecutionValue -= ExecutionConsume;
            chess.EnergyValue -= GetEnergyConsume(skill);
        }

        private float GetEnergyConsume(IActiveSkill skill)
        {
            return EnergyConsume * 100 / (100 + skill.Living);
        }

        public void SetContent(Transform transform)
        {
            transform.AddParagraph($"技能消耗：{EnergyConsume}内力，{ExecutionConsume}行动力。");
        }
    }
}