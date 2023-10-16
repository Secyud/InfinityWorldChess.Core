using InfinityWorldChess.BattleDomain.BattleMapDomain;
using InfinityWorldChess.SkillDomain;
using Secyud.Ugf;

namespace InfinityWorldChess.BattleDomain.BattleSkillDomain
{
    public class BattleCoreSkillCell:BattleSkillCell
    {
        private CoreSkillActionService _actionService;
        
        protected override void Awake()
        {
            base.Awake();
            FormSkillContainer formSkill = 
                Context.Role.NextFormSkills[CellIndex];
            SetSkill(formSkill.FormSkill);
            _actionService = U.Get<CoreSkillActionService>();
        }

        public void OnClick()
        {
            Context.MapAction = _actionService;
            _actionService.CoreSkill = Context.Role.NextCoreSkills[CellIndex];
        }
    }
}