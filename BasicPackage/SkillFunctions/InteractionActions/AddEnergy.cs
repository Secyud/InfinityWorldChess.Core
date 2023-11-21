using InfinityWorldChess.SkillDomain;
using InfinityWorldChess.Ugf;
using Secyud.Ugf.DataManager;
using UnityEngine;

namespace InfinityWorldChess.SkillFunctions
{
    public class AddEnergy : IInteractionAction
    {
        [field: S] public float Factor { get; set; }
        [field: S] public float Value { get; set; }
        public ActiveSkillBase Skill { get; set; }

        public void SetContent(Transform transform)
        {
            transform.AddParagraph(
                $"此招式恢复{Value}+{Factor:P0}[生]点内力。");
        }

        public void Invoke(SkillInteraction interaction)
        {
            interaction.LaunchChess.EnergyValue +=
                Value + Factor * Skill.Living;
        }
    }
}