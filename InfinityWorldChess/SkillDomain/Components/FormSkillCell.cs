using System.Linq;
using InfinityWorldChess.GlobalDomain;
using InfinityWorldChess.RoleDomain;
using InfinityWorldChess.Ugf;
using Secyud.Ugf.TableComponents;

namespace InfinityWorldChess.SkillDomain
{
    public class FormSkillCell: SkillCell<FormSkillCell, FormSkillView>
    {
        public override void OnInstall()
        {
            GlobalScope.Instance.OpenSelect().AutoSetSingleSelectTable
                <IFormSkill, FormSkillSorters, FormSkillFilters>(
                    SkillView.Property.FormSkill.GetLearnedSkills()
                        .Where(u=>Role.FormSkillProperty.CanSet(u,CellIndex))
                            .ToList(), EnsureSkill);
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