using InfinityWorldChess.BattleDomain;
using InfinityWorldChess.Ugf;
using Secyud.Ugf.DataManager;
using UnityEngine;

namespace InfinityWorldChess.SkillDomain
{
    public class SkillEnergyCast : ISkillCastCondition
    {
        [field: S] public float EnergyConsume { get; set; }
        [field: S] public int ExecutionConsume { get; set; }

        public string CheckCastCondition(BattleRole chess, IActiveSkill skill)
        {
            if (chess.ExecutionValue < ExecutionConsume)
                return "行动力不足，无法释放技能。";


            if (chess.EnergyValue < GetEnergyConsume(skill))
                return "内力不足，无法释放技能。";

            return null;
        }

        public void ConditionCast(BattleRole chess, IActiveSkill skill)
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