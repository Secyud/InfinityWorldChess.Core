using InfinityWorldChess.BasicBundle.FormSkills;
using InfinityWorldChess.PlayerDomain;
using Secyud.Ugf.Resource;

namespace InfinityWorldChess.BasicBundle.Interactions
{
    public class AddFormSkill : InteractionAction
    {
        private FormSkillTemplate _skill;

        public override void Invoke()
        {
            if (_skill is not null)
                GameScope.PlayerGameContext.Role.FormSkill.LearnedSkills.Add(_skill);
        }

        public override void SetAction(params string[] message)
        {
            _skill = message[0].CreateAndInit<FormSkillTemplate>();
        }
    }
}