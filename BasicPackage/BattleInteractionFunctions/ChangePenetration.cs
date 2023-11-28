using InfinityWorldChess.BattleInteractionDomain;
using InfinityWorldChess.BuffDomain;
using InfinityWorldChess.FunctionDomain;
using InfinityWorldChess.GlobalDomain;
using InfinityWorldChess.Ugf;
using Secyud.Ugf;
using Secyud.Ugf.DataManager;
using UnityEngine;

namespace InfinityWorldChess.BattleInteractionFunctions
{
    [ID("0F062155-47DE-A9EA-93E2-BDCA7EFB8FD5")]
    public class ChangePenetration : IActionable<BattleInteraction>,
        IHasContent, IPropertyAttached, IHasPriority
    {
        [field: S] public float Value { get; set; }
        [field: S] public float Factor { get; set; }

        public IAttachProperty Property { get; set; }

        public int Priority => -0x10000;

        public void SetContent(Transform transform)
        {
            transform.AddParagraph(
                $"此招式固定增加{BPC.F(Value)}点穿透，且{BPC.P(Factor, "[杀]", "[御]")}点浮动穿透。");
        }

        public void Invoke(BattleInteraction interaction)
        {
            if (Property is not null)
            {
                AttackRecordProperty attackRecord = interaction.GetOrAddAttack();
                attackRecord.Penetration += Value + Factor *
                    (Factor > 0 ? Property.Kiling : Property.Defend);
            }
        }
    }
}