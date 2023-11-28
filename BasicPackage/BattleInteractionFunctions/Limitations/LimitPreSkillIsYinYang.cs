using InfinityWorldChess.BattleDomain;
using InfinityWorldChess.FunctionDomain;
using InfinityWorldChess.SkillDomain;
using InfinityWorldChess.Ugf;
using Secyud.Ugf;
using Secyud.Ugf.DataManager;
using UnityEngine;

namespace InfinityWorldChess.BattleInteractionFunctions
{
    public class LimitPreSkillIsYinYang : ILimitable, IHasContent
    {
        [field: S] public bool IsYang { get; set; }

        public bool CheckUseful()
        {
            CoreSkillContainer skill =
                BattleScope.Instance.Get<CoreSkillActionService>().CoreSkill;
            return skill is not null && skill.EquipLayer > 0 &&
                   (skill.EquipCode >> skill.EquipLayer - 1 & 1) == (IsYang ? 1 : 0);
        }

        public void SetContent(Transform transform)
        {
            transform.AddParagraph($"若前置招式为{(IsYang ? "阳" : "阴")}");
        }
    }
}