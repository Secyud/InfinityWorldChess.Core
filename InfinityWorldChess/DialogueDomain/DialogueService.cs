using InfinityWorldChess.InteractionDomain;
using Secyud.Ugf.AssetComponents;
using Secyud.Ugf.DependencyInjection;

namespace InfinityWorldChess.DialogueDomain
{
    [Registry(DependScope = typeof(InteractionScope))]
    public class DialogueService : IRegistry
    {
        private readonly IMonoContainer<DialoguePanel> _dialoguePanel;

        public DialoguePanel Panel => _dialoguePanel.GetOrCreate();

        public DialogueService(IwcAssets assets)
        {
            _dialoguePanel = MonoContainer<DialoguePanel>.Create(assets);
        }
    }
}