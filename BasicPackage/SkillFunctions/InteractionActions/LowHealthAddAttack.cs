using InfinityWorldChess.BattleDomain;
using InfinityWorldChess.SkillDomain;
using InfinityWorldChess.Ugf;
using Secyud.Ugf.DataManager;
using UnityEngine;

namespace InfinityWorldChess.SkillFunctions
{
    public class LowHealthAddAttack : IInteractionAction
    {
        [field: S] public float Factor { get; set; }

        public ActiveSkillBase Skill { get; set; }

        public void SetContent(Transform transform)
        {
            transform.AddParagraph($"每损失1%的生命值增加{Factor:P0}的攻击系数。");
        }
        public void Invoke(SkillInteraction interaction)
        {
            BattleRole battleRole = interaction.LaunchChess;
            float value = 1 - battleRole.HealthValue / battleRole.MaxHealthValue;
            AttackRecordBuff attackRecord = interaction.GetOrAddAttack();
            attackRecord.AttackFactor += value * Factor * 100;
        }
    }
}