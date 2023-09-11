using Secyud.Ugf.AssetComponents;
using Secyud.Ugf.DependencyInjection;

namespace InfinityWorldChess.InteractionDomain
{
    [Registry(DependScope = typeof(InteractionScope))]
    public class DialogueService 
    {
        private readonly IMonoContainer<DialoguePanel> _dialoguePanel;

        public DialoguePanel Panel => _dialoguePanel.Value;

        public DialogueService(IwcAb ab)
        {
            _dialoguePanel = MonoContainer<DialoguePanel>.Create(ab);
        }

        public  void OpenDialoguePanel()
        {
            _dialoguePanel.Create();
        }
        
        public void CloseDialoguePanel()
        {
            _dialoguePanel.Destroy();
        }
    }
}