using InfinityWorldChess.ActivityDomain;
using InfinityWorldChess.BattleDomain;
using InfinityWorldChess.BattleFunctions;
using InfinityWorldChess.BattleTemplates;
using InfinityWorldChess.DialogueDomain;
using InfinityWorldChess.RoleDomain;
using Secyud.Ugf;
using Secyud.Ugf.DataManager;

namespace InfinityWorldChess.ActivityTemplates
{
    public class BattleActivityTrigger : IActivityTrigger, IDialogueAction
    {
        [field: S] public IObjectAccessor<Role> RoleAccessor { get; set; }
        [field: S] public IObjectAccessor<IBattleDescriptor> BattleAccessor { get; set; }
        [field: S] public string ActionText { get; set; }
        [field: S] public BattleTrigger NextActivity { get; set; }

        private RoleActivityDialogueProperty Property =>
            RoleAccessor?.Value?.GetProperty<RoleActivityDialogueProperty>();

        public void StartActivity(ActivityGroup group, Activity activity)
        {
            Property?.AddAction(this);
        }

        public void FinishActivity(ActivityGroup group, Activity activity)
        {
            Property?.RemoveAction(this);
        }

        public bool VisibleFor(Role role)
        {
            return true;
        }

        public void Invoke()
        {
            IBattleDescriptor battle = BattleAccessor.Value;

            BattleScope.CreateBattle(battle);

            BattleScope instance = BattleScope.Instance;

            instance.Context.BattleFinishAction +=
                ()=> NextActivity.Invoke(instance);
        }
    }
}