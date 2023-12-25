using System.Runtime.InteropServices;
using InfinityWorldChess.BattleDomain;
using InfinityWorldChess.BattleInteractionDomain;
using InfinityWorldChess.BuffDomain;
using InfinityWorldChess.FunctionDomain;
using InfinityWorldChess.Ugf;
using Secyud.Ugf.DataManager;

namespace InfinityWorldChess.BattleInteractionFunctions
{
    [Guid("14BA7BC2-325D-368D-FB65-D1D0D1B66EE2")]
    public class BattleInteractionTrigger : BattleInteractionTriggerBase
    {
        [field: S] public IActionable<BattleInteraction> Actionable { get; set; }
        [field: S] public ILimitable Limit { get; set; }
        
        public override void Invoke(BattleInteraction interaction)
        {
            if (Limit is null || Limit.CheckUseful())
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

        protected override void SetBuff(IBattleUnitBuff buff)
        {
            buff.TryAttach(Limit);
            buff.TryAttach(Actionable);
        }
    }
}