using InfinityWorldChess.BattleDomain;
using InfinityWorldChess.SkillDomain;
using InfinityWorldChess.Ugf;
using Secyud.Ugf.HexMap;
using UnityEngine;

namespace InfinityWorldChess.SkillFunctions
{
    /// <summary>
    /// run an attack.
    /// if not set property yet, use <see cref=""/> instead
    /// </summary>
    public class TreatInteraction: ISkillInteractionEffect,IBuffInteractionEffect
    {
        public int Priority => 0x10000;
        public ActiveSkillBase BelongSkill { get; set; }
        public SkillBuff BelongBuff { get; set; }

        public virtual void SetContent(Transform transform)
        {
            transform.AddParagraph($"对目标进行治疗。");
        }

        public virtual void Invoke(SkillInteraction interaction)
        {
            BattleRole target = interaction.TargetChess;
            float recover = interaction
                .GetOrAddTreat()
                .RunRecover(target);

            HexCell cell = target.Unit.Location;

            BattleScope.Instance.CreateNumberText(cell, (int)recover, Color.green);
        }

        public void Active(SkillInteraction target)
        {
            Invoke(target);
        }
    }
}