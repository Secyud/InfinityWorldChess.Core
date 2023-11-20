using InfinityWorldChess.SkillDomain;
using InfinityWorldChess.Ugf;
using Secyud.Ugf.BasicComponents;
using UnityEngine;

namespace InfinityWorldChess.BattleDomain
{
    public abstract class BattleSkillCell:ShownCell
    {
        [SerializeField] protected SButton Button;
        protected BattleContext Context { get; set; }
        protected string TipText { get; set; }
        
        protected virtual void Awake()
        {
            Context = BattleScope.Instance.Context; 
            SetSkill(Skill);
        }

        protected void SetSkill(IActiveSkill skill)
        {
            BindShowable(skill);
            TipText = skill?.CheckCastCondition(Context.Role,skill);
            Button.interactable = TipText is null;
        }

        protected override void CreateFloating()
        {
            RectTransform content = Skill?.CreateAutoCloseFloatingOnMouse();
            if (TipText is not null)
            {
                SText t = content.AddParagraph(TipText);
                t.color = Color.blue;
            }
        }
        
        protected abstract IActiveSkill Skill { get; }
    }
}