using InfinityWorldChess.BattleDomain;
using InfinityWorldChess.SkillDomain;
using InfinityWorldChess.Ugf;
using Secyud.Ugf.DataManager;
using UnityEngine;

namespace InfinityWorldChess.SkillFunctions
{
    /// <summary>
    /// 延缓敌方行动时间
    /// </summary>
    public class AddTargetTime : IInteractionAction
    {
        [field: S] public float Factor { get; set; }
        [field: S] public float Value { get; set; }

        public ActiveSkillBase Skill { get; set; }

        public void SetContent(Transform transform)
        {
            transform.AddParagraph($"此招式延缓敌方{Value}+{Factor:P0}[灵]点时序。");
        }

        public void Invoke(SkillInteraction interaction)
        {
            BattleRole chess = interaction.TargetChess;
            chess.Time += (int)(Value + Skill.Nimble * Factor);
        }
    }
}