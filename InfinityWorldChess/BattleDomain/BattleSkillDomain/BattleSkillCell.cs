using InfinityWorldChess.SkillDomain;
using InfinityWorldChess.Ugf;
using Secyud.Ugf;
using Secyud.Ugf.BasicComponents;
using UnityEngine;

namespace InfinityWorldChess.BattleDomain.BattleSkillDomain
{
    public class BattleSkillCell:ShownCell
    {
        [SerializeField] private SButton Button;
        public SkillContainer Skill { get; private set; }

        public void OnClick()
        {
            U.Get<SkillObservedService>().Skill = Skill;
        }

        public void RefreshSkill(SkillContainer skill,BattleRole role)
        {
            Skill = skill;
            if (skill?.Skill is null)
            {
                gameObject.SetActive(false);
            }
            else
            {
                gameObject.SetActive(true);
                BindShowable(skill.Skill);
                Button.interactable = skill.Skill.CheckCastCondition(role) is null;
            }
        }
    }
}