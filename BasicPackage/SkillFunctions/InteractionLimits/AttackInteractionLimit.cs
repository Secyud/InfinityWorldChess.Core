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
            if (sender is not SkillInteraction interaction)
            {
                return false;
            }
            AttackRecordBuff attackRecord = interaction.GetAttack();
            return attackRecord.Attacked;
        }

        public void SetContent(Transform transform)
        {
            transform.AddParagraph("如果具有攻击性，则");
        }
    }
}