#region

using System.Diagnostics.CodeAnalysis;

#endregion

namespace InfinityWorldChess.SkillDomain
{
    public class CoreSkillContainer : SkillContainer
    {
        public CoreSkillContainer([NotNull] ICoreSkill skill, byte equipMaxLayer, byte equipCode) :
            base(skill, equipMaxLayer, equipCode, true)
        {
            CoreSkill = skill;
        }

        public ICoreSkill CoreSkill { get; }
    }
}