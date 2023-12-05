using InfinityWorldChess.GameDomain;
using Secyud.Ugf.TableComponents.ButtonComponents;

namespace InfinityWorldChess.SkillDomain
{
    public class CoreSkillPointDivisionButton:ButtonDescriptor<ICoreSkill>
    {
        public override void Invoke()
        {
            GameScope.Instance.OpenPointPanel(Target);
        }

        public override string Name => "分配点数";
        public override bool Visible(ICoreSkill target)
        {
            return true;
        }
    }
}