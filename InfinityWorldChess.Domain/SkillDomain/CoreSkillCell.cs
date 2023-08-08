using InfinityWorldChess.GlobalDomain;
using InfinityWorldChess.Ugf;

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

            for (int i = 0; i < SharedConsts.CoreSkillLayerCount && code >= threshold; i++)
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
                    SkillView.Property.CoreSkill.LearnedSkills,
                    IwcAb.Instance.VerticalCellInk.Value,
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