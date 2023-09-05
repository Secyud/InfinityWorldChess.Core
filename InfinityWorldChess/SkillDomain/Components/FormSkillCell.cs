using InfinityWorldChess.GlobalDomain;
using InfinityWorldChess.Ugf;

namespace InfinityWorldChess.SkillDomain
{
    public class FormSkillCell: SkillCell<FormSkillCell, FormSkillView>
    {
        public override void OnInstall()
        {
            GlobalScope.Instance.OpenSelect().AutoSetSingleSelectTable
                <IFormSkill, FormSkillSorters, FormSkillFilters>(
                    SkillView.Property.FormSkill.LearnedSkills, EnsureSkill);
        }

        public override void OnRemove()
        {
            SkillView.Property.FormSkill.Set(null, CellIndex);
            Bind(null);
        }
        
        private void EnsureSkill(IFormSkill skill)
        {
            SkillView.Property.FormSkill.Set(skill, CellIndex);
            Bind(skill);
        }
    }
}