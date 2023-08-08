using InfinityWorldChess.GlobalDomain;
using InfinityWorldChess.Ugf;

namespace InfinityWorldChess.SkillDomain
{
    public class PassiveSkillCell : SkillCell<PassiveSkillCell, PassiveSkillView>
    {
        public override void OnInstall()
        {
            GlobalScope.Instance.OpenSelect().AutoSetSingleSelectTable
                <IPassiveSkill, PassiveSkillSorters, PassiveSkillFilters>(
                    SkillView.Property.PassiveSkill.LearnedSkills,
                    IwcAb.Instance.VerticalCellInk.Value,
                    EnsureSkill);
        }

        public override void OnRemove()
        {
            SkillView.Property.SetPassiveSkill(null, CellIndex);
            Bind(null);
        }

        private void EnsureSkill(IPassiveSkill skill)
        {
            if (skill is not null)
                for (int i = 0; i < SharedConsts.PassiveSkillCount; i++)
                    if (SkillView.Property.PassiveSkill[i] == skill)
                        SkillView.SkillCells[i].Bind(null);

            SkillView.Property.SetPassiveSkill(skill, CellIndex);
            Bind(skill);
        }
    }
}