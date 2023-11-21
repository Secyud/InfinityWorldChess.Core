using InfinityWorldChess.SkillDomain;

namespace InfinityWorldChess.SkillFunctions
{
    /// <summary>
    /// it can add buff to launcher
    /// </summary>
    public class AddBuffToTarget : BuffWithContent, IInteractionAction
    {
        public ActiveSkillBase Skill { get; set; }

        public void Invoke(SkillInteraction interaction)
        {
            InstallBuff(interaction.TargetChess, Skill);
        }
    }
}