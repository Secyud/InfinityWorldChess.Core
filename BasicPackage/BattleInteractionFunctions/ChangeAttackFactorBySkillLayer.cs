using InfinityWorldChess.BattleDomain;
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
    public class ChangeAttackFactorBySkillLayer : IActionable<BattleInteraction>, IHasContent,IHasPriority
    {
        [field: S] public float Factor { get; set; }
        
        public int Priority => -0x10000;
        public void SetContent(Transform transform)
        {
            transform.AddParagraph($"{BPC.P(Factor)}[招式层数]的攻击系数。");
        }

        public void Invoke(BattleInteraction interaction)
        {
            CoreSkillContainer skill = BattleScope.Instance
                .Get<CoreSkillActionService>().CoreSkill;

            if (skill is not null)
            {
                AttackRecordProperty attackRecord = interaction.GetOrAddAttack();
                attackRecord.AttackFactor += Factor * skill.EquipLayer;
            }
        }
    }
}