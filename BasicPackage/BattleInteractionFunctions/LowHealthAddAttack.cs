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
    public class LowHealthAddAttack : IActionable<BattleInteraction>, IHasContent,IHasPriority
    {
        [field: S] public float Factor { get; set; }
        
        public int Priority => -0x10000;


        public void SetContent(Transform transform)
        {
            transform.AddParagraph($"每损失1%的生命值增加{Factor:P0}的攻击系数。");
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