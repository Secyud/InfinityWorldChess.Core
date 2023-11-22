using InfinityWorldChess.BattleDomain;
using InfinityWorldChess.SkillDomain;
using InfinityWorldChess.Ugf;
using Secyud.Ugf.DataManager;
using UnityEngine;

namespace InfinityWorldChess.SkillFunctions
{
    public class LowHealthAddAttack : ISkillInteractionEffect,IBuffInteractionEffect
    {
        [field: S] public float Factor { get; set; }
        
        public int Priority => 0;

        public ActiveSkillBase BelongSkill { get; set; }
        public SkillBuff BelongBuff { get; set; }

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

        public void Active(SkillInteraction target)
        {
            Invoke(target);
        }
    }
}