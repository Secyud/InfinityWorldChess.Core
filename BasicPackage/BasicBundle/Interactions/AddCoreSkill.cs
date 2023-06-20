using InfinityWorldChess.BasicBundle.CoreSkills;
using InfinityWorldChess.PlayerDomain;
using Secyud.Ugf.Resource;

namespace InfinityWorldChess.BasicBundle.Interactions
{
    public class AddCoreSkill : InteractionAction
    {
        private CoreSkillTemplate _skill;

        public override void Invoke()
        {
            if (_skill is not null)
                GameScope.PlayerGameContext.Role.CoreSkill.LearnedSkills.Add(_skill);
        }

        public override void SetAction(params string[] message)
        {
            _skill = message[0].CreateAndInit<CoreSkillTemplate>();
        }
    }
}