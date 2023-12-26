using InfinityWorldChess.FunctionDomain;
using Secyud.Ugf.DataManager;

namespace InfinityWorldChess.DialogueDomain
{
    public class DialogueOption
    {
        [field: S] public string ShowText { get; set; }
        [field: S] public IActionable Actionable { get; set; }

        public void Invoke()
        {
            Actionable?.Invoke();
        }
    }
}