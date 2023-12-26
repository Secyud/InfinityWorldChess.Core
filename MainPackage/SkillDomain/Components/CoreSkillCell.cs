using System.Linq;
using InfinityWorldChess.GlobalDomain;
using InfinityWorldChess.RoleDomain;
using InfinityWorldChess.Ugf;
using Secyud.Ugf.TableComponents;

namespace InfinityWorldChess.SkillDomain
{
    public class CoreSkillCell : SkillCell<CoreSkillCell, CoreSkillView>
    {
        public byte Layer { get; private set; }
        public byte Code { get; private set; }

        protected override void Awake()
        {
            base.Awake();
            int code = CellIndex;
            int layer = 0;
            int threshold = 2;

            for (int i = 0; i < IWCC.CoreSkillLayerCount && code >= threshold; i++)
            {
                code -= threshold;
                layer++;
                threshold *= 2;
            }

            Code = (byte)code;
            Layer = (byte)layer;
        }


        public override void OnInstall()
        {
            GlobalScope.Instance.OpenSelect().AutoSetSingleSelectTable
                <ICoreSkill, CoreSkillSorters, CoreSkillFilters>(
                    SkillView.Property.CoreSkill.GetLearnedSkills()
                        .Where(u=>Role.CoreSkillProperty.CanSet(u,Layer,Code))
                        .ToList(),
                    EnsureSkill);
        }

        public override void OnRemove()
        {
            SkillView.Property.CoreSkill.Set(null,  Layer, Code);
            Bind(null);
        }

        private void EnsureSkill(ICoreSkill skill)
        {
            SkillView.Property.CoreSkill.Set(skill, Layer, Code);
            Bind(skill);
        }
    }
}