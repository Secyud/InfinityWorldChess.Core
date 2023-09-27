using InfinityWorldChess.BuffDomain;
using InfinityWorldChess.SkillDomain.SkillInteractionDomain;
using Secyud.Ugf;

namespace InfinityWorldChess.SkillEffectDomain.BasicEffectBundle
{
    /// <summary>
    /// actionable effect means effect set on skill interaction.
    /// maybe you want to add damage factor, action on some skill cast.
    /// </summary>
    public interface IActionableEffect:IActionable<SkillInteraction>,IBuffEffect
    {
        
    }
}