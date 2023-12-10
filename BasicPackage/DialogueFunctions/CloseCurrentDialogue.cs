using InfinityWorldChess.DialogueDomain;
using InfinityWorldChess.FunctionDomain;
using Secyud.Ugf;
using UnityEngine;

namespace InfinityWorldChess.DialogueFunctions
{
    public class CloseCurrentDialogue:IActionable
    {
        public void Invoke()
        {
            U.Get<DialogueService>().Panel.Destroy();
        }
    }
}