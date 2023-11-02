using InfinityWorldChess.BuffDomain;
using InfinityWorldChess.SkillDomain.SkillInteractionDomain;

namespace InfinityWorldChess.SkillFunctions.Effect
{
    /// <summary>
    /// actionable effect means effect set on skill interaction.
    /// maybe you want to add damage factor, action on some skill cast.
    /// </summary>
    public interface IActionableEffect:IActionable<SkillInteraction>,IBuffEffect
    {
        
    }
}