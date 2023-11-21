using InfinityWorldChess.BattleDomain;
using InfinityWorldChess.SkillDomain;
using InfinityWorldChess.Ugf;
using UnityEngine;

namespace InfinityWorldChess.SkillFunctions
{
    /// <summary>
    /// it can add buff to launcher
    /// </summary>
    public class AddBuffToLauncher : BuffWithContent,ISkillAction
    {
        public ActiveSkillBase Skill { get; set; }

        public void Invoke(BattleRole battleChess, BattleCell releasePosition)
        {
            InstallBuff(battleChess,Skill);
        }

        public override void SetContent(Transform transform)
        {
            if (BuffTemplate is not null)
            {
                transform.AddParagraph($"为自身添加状态{BuffTemplate.Name}。");
                BuffTemplate.SetContent(transform);
            }
        }
    }
}