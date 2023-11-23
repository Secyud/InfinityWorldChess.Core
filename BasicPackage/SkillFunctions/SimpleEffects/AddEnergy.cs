using InfinityWorldChess.BattleDomain;
using InfinityWorldChess.BuffDomain;
using InfinityWorldChess.SkillDomain;
using InfinityWorldChess.Ugf;
using Secyud.Ugf.DataManager;
using UnityEngine;

namespace InfinityWorldChess.SkillFunctions
{
    public class AddEnergy :
        ISkillInteractionEffect, ISkillActionEffect,
        IBuffInteractionEffect, IBuffActionEffect
    {
        [field: S] public float Factor { get; set; }
        [field: S] public float Value { get; set; }
        public ActiveSkillBase BelongSkill { get; set; }
        public SkillBuff BelongBuff { get; set; }
        public int Priority => 0;

        public void SetContent(Transform transform)
        {
            transform.AddParagraph(
                $"此招式恢复{Value}+{Factor:P0}[生]点内力。");
        }

        public void Invoke(BattleRole battleChess, BattleCell releasePosition)
        {
            Invoke(battleChess);
        }

        public void Invoke(SkillInteraction interaction)
        {
            Invoke(interaction.LaunchChess);
        }

        public void Active(SkillInteraction target)
        {
            Invoke(target.LaunchChess);
        }

        public void Active()
        {
            Invoke(BelongBuff?.Target);
        }

        private void Invoke(BattleRole role)
        {
            IBuffProperty property = BelongSkill ?? BelongBuff.Property;
            if (role is not null && property is not null)
            {
                role.EnergyValue += Value + Factor * property.Living;
            }
        }
    }
}