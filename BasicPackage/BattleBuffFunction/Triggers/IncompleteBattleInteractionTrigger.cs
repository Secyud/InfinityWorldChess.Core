using InfinityWorldChess.BattleInteractionDomain;
using InfinityWorldChess.BuffDomain;
using InfinityWorldChess.FunctionDomain;
using Secyud.Ugf.DataManager;

namespace InfinityWorldChess.BattleBuffFunction
{
    public class IncompleteBattleInteractionTrigger : BattleInteractionTriggerBase
    {
        [field: S] public IActionable Actionable { get; set; }
        [field: S] public ILimitable Limit { get; set; }
        
        public override void Invoke(BattleInteraction interaction)
        {
            if (Limit is null || Limit.CheckUseful())
            {
                Actionable?.Invoke();
                base.Invoke(interaction);
            }
        }

        public override void Attached(IAttachProperty property)
        {
            Property.Attach(Limit);
            Property.Attach(Actionable);
        }
    }
}