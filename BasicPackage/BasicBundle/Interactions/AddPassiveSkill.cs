using InfinityWorldChess.BasicBundle.PassiveSkills;
using InfinityWorldChess.PlayerDomain;
using JetBrains.Annotations;

namespace InfinityWorldChess.BasicBundle.Interactions
{
    public class AddPassiveSkill : InteractionAction
    {
        private PassiveSkillTemplate _skill;

        public override void Invoke()
        {
            if (_skill is not null)
                GameScope.PlayerGameContext.Role.PassiveSkill.LearnedSkills.Add(_skill);
        }

        public override void SetAction([NotNull] params string[] message)
        {
            _skill = message[0].CreateAndInit<PassiveSkillTemplate>();
        }
    }
}