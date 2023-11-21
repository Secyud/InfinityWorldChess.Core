using InfinityWorldChess.BattleDomain;
using InfinityWorldChess.SkillDomain;
using InfinityWorldChess.Ugf;
using Secyud.Ugf.DataManager;
using UnityEngine;

namespace InfinityWorldChess.SkillFunctions
{
    public class CenterAddDamage : IInteractionAction
    {
        [field: S] public int Factor { get; set; }

        public ActiveSkillBase Skill { get; set; }

        public void SetContent(Transform transform)
        {
            transform.AddParagraph($"此招式对落点的攻击额外增加{Factor:p0}。");
        }

        public void Invoke(SkillInteraction interaction)
        {
            if (interaction.TargetChess.Unit.Location == Skill.Cell)
            {
                AttackRecordBuff attackRecord = interaction.GetOrAddAttack();
                attackRecord.AttackFactor += Factor;
            }
        }
    }
}