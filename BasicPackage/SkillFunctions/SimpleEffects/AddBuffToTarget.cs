using InfinityWorldChess.SkillDomain;

namespace InfinityWorldChess.SkillFunctions
{
    /// <summary>
    /// it can add buff to launcher
    /// </summary>
    public class AddBuffToTarget : BuffWithContent,
        ISkillInteractionEffect,IBuffInteractionEffect
    {
        public ActiveSkillBase BelongSkill { get; set; }

        public SkillBuff BelongBuff { get; set; }
        public int Priority => 0x20000;

        public void Invoke(SkillInteraction interaction)
        {
            InstallBuff(interaction.TargetChess,interaction.LaunchChess, BelongSkill);
        }
        public void Active(SkillInteraction target)
        {
            InstallBuff(target.TargetChess,target.LaunchChess, BelongBuff.Property);
        }
    }
}