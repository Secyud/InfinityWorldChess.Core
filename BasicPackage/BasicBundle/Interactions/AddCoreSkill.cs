using InfinityWorldChess.BasicBundle.CoreSkills;
using InfinityWorldChess.PlayerDomain;
using Secyud.Ugf.DataManager;

namespace InfinityWorldChess.BasicBundle.Interactions
{
    public class AddCoreSkill : InteractionAction
    {
        [field: S(ID = 0)]public string SkillName;

        public override void Invoke()
        {
            if (SkillName is null) return;
            CoreSkillTemplate skill = DataObject.Create<CoreSkillTemplate>(SkillName);
            GameScope.Instance.Player.Role.CoreSkill.LearnedSkills.Add(skill);
        }
    }
}