#region

using InfinityWorldChess.BattleDomain.BattleSkillDomain;
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
        [SerializeField] private BattleSkillCell[] CoreSkillCells;
        [SerializeField] private BattleSkillCell[] FormSkillCells;
        private CoreSkillContainer[] _nextCoreSkills;
        private FormSkillContainer[] _nextFormSkills;
        private RoleObservedService _roleObservedService;
        private SkillObservedService _skillObservedService;
        private void Awake()
        {
            _roleObservedService = U.Get<RoleObservedService>();
            _skillObservedService = U.Get<SkillObservedService>();
            _skillObservedService.AddObserverObject(nameof(BattlePlayerController), RefreshUi,gameObject);
            RefreshUi();
        }

        public void OnHoverFormSkill(int index)
        {
            IActiveSkill content = _nextFormSkills?[index].Skill;
            if (content is not null)
            {
                RectTransform c = content.CreateAutoCloseFloatingOnMouse();
                string tip = content.CheckCastCondition(_roleObservedService.Role);
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
                string tip = content.CheckCastCondition(_roleObservedService.Role);
                if (tip is not null)
                {
                    SText t = c.AddParagraph(tip);
                    t.color = Color.blue;
                }
            }
        }

        public void OnCoreSkillClick(int index)
        {
            CoreSkillContainer skill = _roleObservedService.Role.NextCoreSkills[index];
            if (skill is not null) _skillObservedService.Skill = skill;
        }

        public void OnFormSkillClick(int index)
        {
            FormSkillContainer skill = _roleObservedService.Role.NextFormSkills[index];
            if (skill is not null) _skillObservedService.Skill = skill;
        }

        public void OnCoreSkillReset()
        {
            if (_roleObservedService.Role.ResetCoreSkill())
                _skillObservedService.AutoReselectSkill();
            else
                "行动力不足。无法执行操作。".CreateTipFloatingOnCenter();
        }

        public void OnFormSkillReset()
        {
            if (_roleObservedService.Role.ResetFormSkill())
                _skillObservedService.AutoReselectSkill();
            else
                "行动力不足。无法执行操作。".CreateTipFloatingOnCenter();
        }
        public void ExitPlayerControl()
        {
            BattleScope.Instance.Map.ExitControl();
            gameObject.SetActive(false);
        }

        public void FinishBattle()
        {
            U.Get<BattleGlobalService>().DestroyBattle();
        }

        public void RefreshUi()
        {
            BattleRole battleRole = _roleObservedService.Role;
            _nextCoreSkills = battleRole.NextCoreSkills;
            _nextFormSkills = battleRole.NextFormSkills;
            for (int i = 0; i < SharedConsts.CoreSkillCodeCount; i++)
            {
                CoreSkillContainer skill = _nextCoreSkills[i];
                CoreSkillCells[i].RefreshSkill(skill,battleRole);
            }
            for (int i = 0; i < SharedConsts.FormSkillTypeCount; i++)
            {
                FormSkillContainer skill = _nextFormSkills[i];
                FormSkillCells[i].RefreshSkill(skill,battleRole);
            }
        }
    }
}