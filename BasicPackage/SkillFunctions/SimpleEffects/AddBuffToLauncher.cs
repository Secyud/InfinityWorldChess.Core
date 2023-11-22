using InfinityWorldChess.BattleDomain;
using InfinityWorldChess.SkillDomain;
using InfinityWorldChess.Ugf;
using UnityEngine;

namespace InfinityWorldChess.SkillFunctions
{
    /// <summary>
    /// it can add buff to launcher
    /// </summary>
    public class AddBuffToLauncher : BuffWithContent,
        ISkillInteractionEffect, ISkillActionEffect, 
        IBuffInteractionEffect, IBuffActionEffect
    {
        public ActiveSkillBase BelongSkill { get; set; }
        public SkillBuff BelongBuff { get; set; }

        public int Priority => 0x20000;

        public override void SetContent(Transform transform)
        {
            if (BuffTemplate is not null)
            {
                transform.AddParagraph($"为自身添加状态{BuffTemplate.Name}。");
                BuffTemplate.SetContent(transform);
            }
        }

        public void Invoke(BattleRole battleChess, BattleCell releasePosition)
        {
            InstallBuff(battleChess,battleChess, BelongSkill);
        }

        public void Invoke(SkillInteraction interaction)
        {
            InstallBuff(interaction.LaunchChess,interaction.LaunchChess, BelongSkill);
        }

        public void Active(SkillInteraction target)
        {
            InstallBuff(target.LaunchChess,target.LaunchChess, BelongBuff?.Property);
        }

        public void Active()
        {
            InstallBuff(BelongBuff?.Target,BelongBuff?.Target, BelongBuff?.Property);
        }
    }
}