using InfinityWorldChess.BattleDomain;
using InfinityWorldChess.BuffDomain;
using InfinityWorldChess.SkillDomain;
using InfinityWorldChess.Ugf;
using Secyud.Ugf.DataManager;
using UnityEngine;

namespace InfinityWorldChess.SkillFunctions
{
    public class InteractTrigger : InteractionTrigger
    {
        [field:S] public byte InteractionType { get; set; }
        
        protected ActionableContainer<SkillInteraction> Container { get; set; }

        public override void Install(BattleRole target)
        {
            BattleEvents events = target.GetProperty<BattleEvents>();
            Container = InteractionType switch
            {
                0 => events.PrepareLaunch,
                1 => events.PrepareReceive,
                2 => events.LaunchCallback,
                3 => events.ReceiveCallback,
                _ => events.PrepareLaunch
            };
            Container?.Add(this);
        }

        public override void UnInstall(BattleRole target)
        {
            Container?.Remove(this);
        }

        public override void SetContent(Transform transform)
        {
            transform.AddParagraph("每次受到技能触发。");
            base.SetContent(transform);
        }
    }
}