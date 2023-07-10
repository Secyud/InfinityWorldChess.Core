#region

using InfinityWorldChess.SkillDomain;
using InfinityWorldChess.Ugf;
using Secyud.Ugf.BasicComponents;
using UnityEngine;

#endregion

namespace InfinityWorldChess.BattleDomain
{
    public class BattlePlayerController : MonoBehaviour
    {
        [SerializeField] private ImageCell[] CoreSkillCells;
        [SerializeField] private ImageCell[] FormSkillCells;
        private CoreSkillContainer[] _nextCoreSkills;
        private FormSkillContainer[] _nextFormSkills;
        private BattleContext _context;
        private BattleContext Context => _context ??= BattleScope.Instance.Context;

        private void Awake()
        {
            if (Context is null)
                return;
            RoleBattleChess role = Context.CurrentRole;
            _nextCoreSkills = role.NextCoreSkills;
            _nextFormSkills = role.NextFormSkills;
            for (int i = 0; i < SharedConsts.CoreSkillCodeCount; i++)
            {
                CoreSkillContainer skill = _nextCoreSkills[i];
                if (skill is null)
                    CoreSkillCells[i].gameObject.SetActive(false);
                else
                {
                    CoreSkillCells[i].gameObject.SetActive(true);
                    CoreSkillCells[i].OnInitialize(skill.Skill);
                    SButton button = CoreSkillCells[i].GetComponent<SButton>();
                    button.interactable =
                        skill.Skill.CheckCastCondition(role) is null;
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
                    FormSkillCells[i].OnInitialize(skill.Skill);
                    SButton button = FormSkillCells[i].GetComponent<SButton>();
                    button.interactable =
                        skill.Skill.CheckCastCondition(role) is null;
                }
            }
        }

        public void OnHoverFormSkill(int index)
        {
            IActiveSkill content = _nextFormSkills?[index].Skill;
            if (content is not null)
            {
                RectTransform c = content.CreateFloating();
                string tip = content.CheckCastCondition(Context.CurrentRole);
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
                RectTransform c = content.CreateFloating();
                string tip = content.CheckCastCondition(Context.CurrentRole);
                if (tip is not null)
                {
                    SText t = c.AddParagraph(tip);
                    t.color = Color.blue;
                }
            }
        }

        public void OnCoreSkillClick(int index)
        {
            CoreSkillContainer skill = Context.CurrentRole.NextCoreSkills[index];
            if (skill is not null) Context.CurrentSkill = skill;
        }

        public void OnFormSkillClick(int index)
        {
            FormSkillContainer skill = Context.CurrentRole.NextFormSkills[index];
            if (skill is not null) Context.CurrentSkill = skill;
        }

        public void OnCoreSkillReset()
        {
            if (Context.CurrentRole.ResetCoreSkill())
                Context.EnterPlayerControl();
            else
                "行动力不足。无法执行操作。".CreateTipFloatingOnCenter();
        }

        public void OnFormSkillReset()
        {
            if (Context.CurrentRole.ResetCoreSkill())
                Context.EnterPlayerControl();
            else
                "行动力不足。无法执行操作。".CreateTipFloatingOnCenter();
        }

        public void ExitPlayerControl()
        {
            Context.ExitPlayerControl();
        }

        public void FinishBattle()
        {
            Context.Battle.OnBattleFinish();
        }
    }
}