using InfinityWorldChess.BasicBundle.FormSkills;
using InfinityWorldChess.PlayerDomain;
using Secyud.Ugf.DataManager;

namespace InfinityWorldChess.BasicBundle.Interactions
{
    public class AddFormSkill : InteractionAction
    {
        [field: S(ID = 0)]public string SkillName;

        public override void Invoke()
        {
            if (SkillName is null) return;
            FormSkillTemplate skill = DataObject.Create<FormSkillTemplate>(SkillName);
            GameScope.Instance.Player.Role.FormSkill.LearnedSkills.Add(skill);
        }
    }
}