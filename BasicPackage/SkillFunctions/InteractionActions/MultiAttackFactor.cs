using InfinityWorldChess.BattleDomain;
using InfinityWorldChess.SkillDomain;
using InfinityWorldChess.Ugf;
using Secyud.Ugf.DataManager;
using UnityEngine;

namespace InfinityWorldChess.SkillFunctions
{
    public class MultiAttackFactor : IInteractionAction
    {
        [field: S] public float Factor { get; set; }
        [field: S] public bool IsYang { get; set; }

        public ActiveSkillBase Skill { get; set; }

        public void SetContent(Transform transform)
        {
            transform.AddParagraph($"若前置招式为{(IsYang ? "阳" : "阴")}，攻击系数乘{1 + Factor:P0}。");
        }
        public void Invoke(SkillInteraction interaction)
        {
            CoreSkillContainer skill = 
                BattleScope.Instance.Get<CoreSkillActionService>().CoreSkill;
            if (skill is not null &&
                (skill.EquipCode >> skill.EquipLayer - 1 & 1) == (IsYang?1:0))
            {
                AttackRecordBuff attackRecord = interaction.GetOrAddAttack();
                attackRecord.AttackFactor *= 1 + Factor;
            }
        }
    }
}