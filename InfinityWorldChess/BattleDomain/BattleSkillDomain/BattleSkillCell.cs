using InfinityWorldChess.SkillDomain;
using InfinityWorldChess.Ugf;
using Secyud.Ugf;
using Secyud.Ugf.BasicComponents;
using UnityEngine;

namespace InfinityWorldChess.BattleDomain.BattleSkillDomain
{
    public abstract class BattleSkillCell:ShownCell
    {
        [SerializeField] protected SButton Button;
        protected BattleContext Context { get; set; }
        protected string TipText { get; set; }
        
        protected virtual void Awake()
        {
            Context = BattleScope.Instance.Context;
        }

        protected void SetSkill(IActiveSkill skill)
        {
            BindShowable(skill);
            TipText = skill?.CheckCastCondition(Context.Role);
            Button.interactable = TipText is null;
        }

        protected override void CreateFloating()
        {
            FormSkillContainer formSkill = 
                Context.Role.NextFormSkills[CellIndex];
            RectTransform content = formSkill.FormSkill.CreateAutoCloseFloatingOnMouse();
            if (TipText is not null)
            {
                SText t = content.AddParagraph(TipText);
                t.color = Color.blue;
            }
        }
    }
}