using InfinityWorldChess.BattleDomain;
using InfinityWorldChess.BattleInteractionDomain;
using InfinityWorldChess.BuffDomain;
using InfinityWorldChess.FunctionDomain;
using InfinityWorldChess.Ugf;
using Secyud.Ugf.DataManager;

namespace InfinityWorldChess.BattleInteractionFunctions
{
    [ID("8DB6A9A1-DE9C-4BD1-933D-F44547D8B3B2")]
    public class BattleInteractionTriggerFull : BattleInteractionTriggerBase
    {
        [field: S] public IActionable<BattleInteraction> Actionable { get; set; }
        [field: S] public ILimitable<BattleInteraction> Limit { get; set; }
        
        public override void Invoke(BattleInteraction interaction)
        {
            if (Limit is null || Limit.CheckUseful(interaction))
            {
                Actionable?.Invoke(interaction);
                base.Invoke(interaction);
            }
        }

        public override void Attached(IAttachProperty property)
        {
            property.TryAttach(Limit);
            property.TryAttach(Actionable);
        }

        protected override void SetBuff(IBattleRoleBuff buff)
        {
            buff.TryAttach(Limit);
            buff.TryAttach(Actionable);
        }
    }
}