using InfinityWorldChess.SkillDomain;
using Secyud.Ugf;

namespace InfinityWorldChess.BattleDomain
{
    public class BattleCoreSkillCell:BattleSkillCell
    {
        private CoreSkillActionService _actionService;
        
        protected override void Awake()
        {
            base.Awake();
            _actionService = U.Get<CoreSkillActionService>();
        }

        protected override IActiveSkill Skill => CoreSkill?.CoreSkill;

        protected CoreSkillContainer CoreSkill => Context.Role.NextCoreSkills[CellIndex];
        
        public void OnClick()
        {
            Context.MapAction = _actionService;
            _actionService.CoreSkill = CoreSkill;
        }
    }
}