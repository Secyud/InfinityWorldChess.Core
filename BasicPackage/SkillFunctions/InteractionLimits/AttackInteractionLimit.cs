using InfinityWorldChess.SkillDomain;
using InfinityWorldChess.Ugf;
using Secyud.Ugf;
using UnityEngine;

namespace InfinityWorldChess.SkillFunctions.InteractionLimits
{
    public class AttackInteractionLimit : ILimitCondition, IHasContent
    {
        public bool CheckLimit(object sender)
        {
            return sender is SkillInteraction interaction &&
                   interaction.GetAttack() is not null;
        }

        public void SetContent(Transform transform)
        {
            transform.AddParagraph("如果具有攻击性，则");
        }
    }
}