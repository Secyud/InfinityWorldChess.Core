#region

using InfinityWorldChess.BattleDomain.BattleSkillDomain;
using InfinityWorldChess.GameDomain;
using InfinityWorldChess.SkillDomain;
using InfinityWorldChess.Ugf;
using Secyud.Ugf;
using Secyud.Ugf.BasicComponents;
using UnityEngine;

#endregion

namespace InfinityWorldChess.BattleDomain.BattleRoleDomain
{
    public class BattlePlayerController : MonoBehaviour
    {
        [SerializeField] private ShownCell[] CoreSkillCells;
        [SerializeField] private ShownCell[] FormSkillCells;
        private CoreSkillContainer[] _nextCoreSkills;
        private FormSkillContainer[] _nextFormSkills;
        private RoleRefreshService _roleRefreshService;
        private SkillRefreshService _skillRefreshService;
        private void Awake()
        {
            _roleRefreshService = U.Get<RoleRefreshService>();
            _skillRefreshService = U.Get<SkillRefreshService>();
            BattleRole battleRole = _roleRefreshService.Role;
            _nextCoreSkills = battleRole.NextCoreSkills;
            _nextFormSkills = battleRole.NextFormSkills;
            for (int i = 0; i < SharedConsts.CoreSkillCodeCount; i++)
            {
                CoreSkillContainer skill = _nextCoreSkills[i];
                if (skill is null)
                    CoreSkillCells[i].gameObject.SetActive(false);
                else
                {
                    CoreSkillCells[i].gameObject.SetActive(true);
                    CoreSkillCells[i].BindShowable(skill.Skill);
                    SButton button = CoreSkillCells[i].GetComponent<SButton>();
                    button.interactable =
                        skill.Skill.CheckCastCondition(battleRole) is null;
                }
            }
            for (int i = 0; i < SharedConsts.FormSkillTypeCount; i++)
            {
                FormSkillContainer skill = _nextFormSkills[i];

                if (skill?.Skill is null)
                    FormSkillCells[i].gameObject.SetActive(false);
                else
                {
                    FormSkillCells[i].gameObject.SetActive(true);
                    FormSkillCells[i].BindShowable(skill.Skill);
                    SButton button = FormSkillCells[i].GetComponent<SButton>();
                    button.interactable =
                        skill.Skill.CheckCastCondition(battleRole) is null;
                }
            }
        }

        public void OnHoverFormSkill(int index)
        {
            IActiveSkill content = _nextFormSkills?[index].Skill;
            if (content is not null)
            {
                RectTransform c = content.CreateAutoCloseFloatingOnMouse();
                string tip = content.CheckCastCondition(_roleRefreshService.Role);
                if (tip is not null)
                {
                    SText t = c.AddParagraph(tip);
                    t.color = Color.blue;
                }
            }
        }

        public void OnHoverCoreSkill(int index)
        {
            IActiveSkill content = _nextCoreSkills?[index].Skill;
            if (content is not null)
            {
                RectTransform c = content.CreateAutoCloseFloatingOnMouse();
                string tip = content.CheckCastCondition(_roleRefreshService.Role);
                if (tip is not null)
                {
                    SText t = c.AddParagraph(tip);
                    t.color = Color.blue;
                }
            }
        }

        public void OnCoreSkillClick(int index)
        {
            CoreSkillContainer skill = _roleRefreshService.Role.NextCoreSkills[index];
            if (skill is not null) _skillRefreshService.Skill = skill;
        }

        public void OnFormSkillClick(int index)
        {
            FormSkillContainer skill = _roleRefreshService.Role.NextFormSkills[index];
            if (skill is not null) _skillRefreshService.Skill = skill;
        }

        public void OnCoreSkillReset()
        {
            if (_roleRefreshService.Role.ResetCoreSkill())
                BattleScope.Instance.Map.EnterControl();
            else
                "行动力不足。无法执行操作。".CreateTipFloatingOnCenter();
        }

        public void OnFormSkillReset()
        {
            if (_roleRefreshService.Role.ResetFormSkill())
                BattleScope.Instance.Map.EnterControl();
            else
                "行动力不足。无法执行操作。".CreateTipFloatingOnCenter();
        }

        public void ExitPlayerControl()
        {
            BattleScope.Instance.Map.ExitControl();
        }

        public void FinishBattle()
        {
            U.Get<BattleGlobalService>().DestroyBattle();
        }
    }
}