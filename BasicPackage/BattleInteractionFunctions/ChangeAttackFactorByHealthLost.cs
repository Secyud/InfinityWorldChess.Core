using InfinityWorldChess.BattleDomain;
using InfinityWorldChess.BattleInteractionDomain;
using InfinityWorldChess.FunctionDomain;
using InfinityWorldChess.GlobalDomain;
using InfinityWorldChess.Ugf;
using Secyud.Ugf;
using Secyud.Ugf.DataManager;
using UnityEngine;

namespace InfinityWorldChess.BattleInteractionFunctions
{
    [ID("1192A36C-5697-D24C-9B42-71E3F2755433")]
    public class ChangeAttackFactorByHealthLost : IActionable<BattleInteraction>, IHasContent,IHasPriority
    {
        [field: S] public float Factor { get; set; }
        
        public int Priority => -0x10000;

        public void SetContent(Transform transform)
        {
            transform.AddParagraph($"每损失1%的生命值{BPC.P(Factor)}的攻击系数。");
        }
        public void Invoke(BattleInteraction interaction)
        {
            BattleRole battleRole = interaction.Origin;
            float value = 1 - battleRole.HealthValue / battleRole.MaxHealthValue;
            AttackRecordProperty attackRecord = interaction.GetOrAddAttack();
            attackRecord.AttackFactor += value * Factor * 100;
        }
    }
}