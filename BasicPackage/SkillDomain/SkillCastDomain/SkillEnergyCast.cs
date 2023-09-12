using InfinityWorldChess.BattleDomain;
using Secyud.Ugf.DataManager;

namespace InfinityWorldChess.SkillDomain.SkillCastDomain
{
    public class SkillEnergyCast:ISkillCastCondition
    {
        [field:S] public float EnergyConsume { get; set; }
        [field:S] public int ExecutionConsume { get; set; }
        public string ShowDescription => $"内力: {EnergyConsume}, 行动力: {ExecutionConsume}";
        public string CheckCastCondition(BattleRole chess,IActiveSkill skill)
        {
            if (chess.ExecutionValue < ExecutionConsume)
                return "行动力不足，无法释放技能。";

            
            if (chess.EnergyValue < GetEnergyConsume(skill))
                return "内力不足，无法释放技能。";
            
            return null;
        }

        public void ConditionCast(BattleRole chess,IActiveSkill skill)
        {
            chess.ExecutionValue -= ExecutionConsume;
            chess.EnergyValue -= GetEnergyConsume(skill);
        }

        private float GetEnergyConsume(IActiveSkill skill)
        {
            return EnergyConsume * 100 / (100 + skill.Living);
        }
    }
}