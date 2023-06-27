using InfinityWorldChess.BasicBundle.PassiveSkills;
using InfinityWorldChess.PlayerDomain;
using Secyud.Ugf.DataManager;

namespace InfinityWorldChess.BasicBundle.Interactions
{
    public class AddPassiveSkill : InteractionAction
    {
        [field: S(ID = 0)]public string SkillName;

        public override void Invoke()
        {
            if (SkillName is null) return;
            PassiveSkillTemplate skill = DataObject.Create<PassiveSkillTemplate>(SkillName);
            GameScope.Instance.Player.Role.PassiveSkill.LearnedSkills.Add(skill);
        }
    }
}