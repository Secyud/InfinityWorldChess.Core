using System;
using System.Collections.Generic;
using System.Linq;
using InfinityWorldChess.DialogueDomain;
using InfinityWorldChess.FunctionDomain;
using InfinityWorldChess.GlobalDomain;
using InfinityWorldChess.RoleDomain;

namespace InfinityWorldChess.ActivityDomain
{
    public sealed class RoleActivityDialogueProperty : IRoleProperty, ILimitable
    {
        public List<DialogueOption> DialogueActions { get; } = new();

        public bool CheckUseful()
        {
            return DialogueActions.Any();
        }

        public void Install(Role target)
        {
        }

        public void UnInstall(Role target)
        {
        }

        public void Overlay(IOverlayable<Role> otherOverlayable)
        {
        }

        public Type Id => GetType();
    }
}