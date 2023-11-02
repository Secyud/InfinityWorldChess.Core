using System.Collections.Generic;
using System.Linq;
using InfinityWorldChess.DialogueDomain;
using InfinityWorldChess.InteractionDomain;
using InfinityWorldChess.RoleDomain;

namespace InfinityWorldChess.ActivityDomain
{
    public sealed class RoleActivityDialogueProperty:RoleProperty
    {
        private readonly List<IDialogueAction> _actions = new();

        public IReadOnlyList<IDialogueAction> DialogueActions => _actions;
        
        public void AddAction(IDialogueAction action)
        {
            _actions.Add(action);
        }

        public void RemoveAction(IDialogueAction action)
        {
            _actions.Remove(action);
            if (!_actions.Any())
            {
                Role.RemoveProperty<RoleActivityDialogueProperty>();
            }
        }

        public override bool CheckNeeded()
        {
            return _actions.Any();
        }
    }
}