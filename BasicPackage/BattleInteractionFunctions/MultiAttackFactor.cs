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
    public class MultiAttackFactor : IActionable<BattleInteraction>, IHasContent, IHasPriority
    {
        [field: S] public float Factor { get; set; }
        [field: S] public bool IsYang { get; set; }

        public int Priority => -10000;

        public void SetContent(Transform transform)
        {
            transform.AddParagraph($"若前置招式为{(IsYang ? "阳" : "阴")}，攻击系数乘{1 + Factor:P0}。");
        }

        /// <summary>
        /// use core skill container to get real pre skill
        /// </summary>
        /// <param name="interaction"></param>
        public void Invoke(BattleInteraction interaction)
        {
            CoreSkillContainer skill =
                BattleScope.Instance.Get<CoreSkillActionService>().CoreSkill;
            if (skill is not null &&
                (skill.EquipCode >> skill.EquipLayer - 1 & 1) == (IsYang ? 1 : 0))
            {
                AttackRecordProperty attackRecord = interaction.GetOrAddAttack();
                attackRecord.AttackFactor *= 1 + Factor;
            }
        }
    }
}