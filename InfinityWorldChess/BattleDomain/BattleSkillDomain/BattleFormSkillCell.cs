using InfinityWorldChess.SkillDomain;
using Secyud.Ugf;

namespace InfinityWorldChess.BattleDomain
{
    public class BattleFormSkillCell:BattleSkillCell
    {
        private FormSkillActionService _actionService;
        
        protected override void Awake()
        {
            base.Awake();
            _actionService = U.Get<FormSkillActionService>();
        }

        protected override IActiveSkill Skill => FormSkill?.FormSkill;

        protected FormSkillContainer FormSkill => Context.Unit.NextFormSkills[CellIndex];
        public void OnClick()
        {
            Context.MapAction = _actionService;
            _actionService.FormSkill = FormSkill;
        }
    }
}