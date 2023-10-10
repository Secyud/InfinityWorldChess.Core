using InfinityWorldChess.RoleDomain;
using Secyud.Ugf;
using Secyud.Ugf.AssetLoading;
using System;
using System.Collections.Generic;
using Secyud.Ugf.AssetComponents;
using Secyud.Ugf.DataManager;
using UnityEngine;

namespace InfinityWorldChess.InteractionDomain
{
    public class DialogueUnit : IDialogueUnit
    {
        [field: S] public List<IDialogueAction> ActionList { get; } = new();
        [field: S] public IDialogueAction DefaultAction { get; set; } = new CloseDialogueAction();
        [field: S] public string Text { get; set; }
    }
}