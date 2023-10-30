using System.Linq;
using InfinityWorldChess.GameDomain;
using InfinityWorldChess.RoleDomain;
using Secyud.Ugf;
using Secyud.Ugf.TableComponents.ButtonComponents;

namespace InfinityWorldChess.InteractionDomain.ChatDomain
{
    public class ChatButtonDescriptor : ButtonDescriptor<Role>
    {
        public override void Invoke()
        {
            DialogueService service = U.Get<DialogueService>();
            service.OpenDialoguePanel();
            ChatRegister register = U.Get<ChatRegister>();
            ChatDialogueUnit unit = new();
            GameScope.Instance.Role.MainOperationRole = Target;
            foreach (IDialogueAction item in register.Items
                         .Where(item => item.VisibleFor(Target)))
            {
                unit.ActionList.Add(item);
            }
            service.Panel.SetInteraction(unit);
        }

        public override string Name => "闲聊";

        public override bool Visible(Role target)
        {
            return true;
        }
    }
}