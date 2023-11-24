using InfinityWorldChess.BattleInteractionDomain;
using InfinityWorldChess.BuffDomain;
using InfinityWorldChess.FunctionDomain;
using InfinityWorldChess.GlobalDomain;
using InfinityWorldChess.SkillDomain;
using InfinityWorldChess.Ugf;
using Secyud.Ugf;
using Secyud.Ugf.DataManager;
using UnityEngine;

namespace InfinityWorldChess.BattleInteractionFunctions
{
    public class CenterAddDamage : IActionable<BattleInteraction>,
        IHasContent, IPropertyAttached,IHasPriority
    {
        [field: S] public int Factor { get; set; }
        public IAttachProperty Property { get; set; }


        public int Priority => -0x10000;
        public void SetContent(Transform transform)
        {
            transform.AddParagraph($"此招式对落点的攻击额外增加{Factor:p0}。");
        }

        public void Invoke(BattleInteraction interaction)
        {
            if (Property is ActiveSkillBase activeSkillBase &&
                interaction.Target.Unit.Location == activeSkillBase.Cell)
            {
                AttackRecordProperty attackRecord = interaction.GetOrAddAttack();
                attackRecord.AttackFactor += Factor;
            }
        }
    }
}