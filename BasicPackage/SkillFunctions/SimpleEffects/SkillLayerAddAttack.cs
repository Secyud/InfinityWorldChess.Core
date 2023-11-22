using InfinityWorldChess.BattleDomain;
using InfinityWorldChess.SkillDomain;
using InfinityWorldChess.Ugf;
using Secyud.Ugf.DataManager;
using UnityEngine;

namespace InfinityWorldChess.SkillFunctions
{
    public class SkillLayerAddAttack : ISkillInteractionEffect
    {
        [field: S] public float Factor { get; set; }
        public ActiveSkillBase BelongSkill { get; set; }

        public void SetContent(Transform transform)
        {
            transform.AddParagraph($"增加此招式技能层数*{Factor:P0}的攻击系数。");
        }

        public void Invoke(SkillInteraction interaction)
        {
            CoreSkillContainer skill = BattleScope.Instance
                .Get<CoreSkillActionService>().CoreSkill;

            if (skill is not null)
            {
                AttackRecordBuff attackRecord = interaction.GetOrAddAttack();
                attackRecord.AttackFactor += Factor * skill.EquipLayer;
            }
        }
    }
}