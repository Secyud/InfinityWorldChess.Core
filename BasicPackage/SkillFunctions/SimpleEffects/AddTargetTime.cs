using InfinityWorldChess.BattleDomain;
using InfinityWorldChess.SkillDomain;
using InfinityWorldChess.Ugf;
using Secyud.Ugf.DataManager;
using UnityEngine;

namespace InfinityWorldChess.SkillFunctions
{
    /// <summary>
    /// 延缓敌方行动时间
    /// </summary>
    public class AddTargetTime : ISkillInteractionEffect, IBuffInteractionEffect
    {
        [field: S] public float Factor { get; set; }
        [field: S] public float Value { get; set; }

        public ActiveSkillBase BelongSkill { get; set; }
        public SkillBuff BelongBuff { get; set; }
        public int Priority => 0x20000;

        public void SetContent(Transform transform)
        {
            transform.AddParagraph($"此招式延缓敌方{Value}+{Factor:P0}[灵]点时序。");
        }

        public void Invoke(SkillInteraction interaction)
        {
            BattleRole chess = interaction.TargetChess;
            chess.Time += (int)(Value + BelongSkill.Nimble * Factor);
        }

        public void Active(SkillInteraction target)
        {
            Invoke(target);
        }
    }
}