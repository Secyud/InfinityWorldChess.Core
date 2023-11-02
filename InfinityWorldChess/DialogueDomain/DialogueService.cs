using InfinityWorldChess.InteractionDomain;
using Secyud.Ugf.AssetComponents;
using Secyud.Ugf.DependencyInjection;

namespace InfinityWorldChess.DialogueDomain
{
    [Registry(DependScope = typeof(InteractionScope))]
    public class DialogueService : IRegistry
    {
        private readonly IMonoContainer<DialoguePanel> _dialoguePanel;

        public DialoguePanel Panel
        {
            get
            {
                if (!_dialoguePanel.Value)
                {
                    OpenDialoguePanel();
                }
                return _dialoguePanel.Value;
            }
        }

        public DialogueService(IwcAssets assets)
        {
            _dialoguePanel = MonoContainer<DialoguePanel>.Create(assets);
        }

        public void OpenDialoguePanel()
        {
            _dialoguePanel.Create();
        }

        public void CloseDialoguePanel()
        {
            _dialoguePanel.Destroy();
        }
    }
}