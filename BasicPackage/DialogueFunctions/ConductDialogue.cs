using System.Runtime.InteropServices;
using InfinityWorldChess.DialogueDomain;
using InfinityWorldChess.FunctionDomain;
using InfinityWorldChess.InteractionDomain;
using InfinityWorldChess.Ugf;
using Secyud.Ugf;
using Secyud.Ugf.DataManager;
using UnityEngine;

namespace InfinityWorldChess.DialogueFunctions
{
    [Guid("E004417F-54C2-F296-A828-4C8C643DF453")]
    public class ConductDialogue:IActionable,IHasContent
    {
        [field: S] public IObjectAccessor<IDialogueUnit> UnitAccessor { get; set; }
        
        public void Invoke()
        {
            InteractionScope.Instance.DialogueService.Panel
                .SetInteraction(UnitAccessor.Value);
        }

        public void SetContent(Transform transform)
        {
            transform.AddParagraph("与之进行对话。");
        }
    }
}