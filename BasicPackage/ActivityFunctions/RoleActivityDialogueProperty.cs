using System;
using System.Collections.Generic;
using System.Linq;
using InfinityWorldChess.DialogueDomain;
using InfinityWorldChess.FunctionDomain;
using InfinityWorldChess.RoleDomain;

namespace InfinityWorldChess.ActivityFunctions
{
    public sealed class RoleActivityDialogueProperty : IRoleProperty, ILimitable
    {
        public List<DialogueOption> DialogueActions { get; } = new();

        public bool CheckUseful()
        {
            return DialogueActions.Any();
        }

        public void InstallFrom(Role target)
        {
        }

        public void UnInstallFrom(Role target)
        {
        }

        public void Overlay(IOverlayable<Role> otherOverlayable)
        {
        }

        public Type Id => GetType();
    }
}