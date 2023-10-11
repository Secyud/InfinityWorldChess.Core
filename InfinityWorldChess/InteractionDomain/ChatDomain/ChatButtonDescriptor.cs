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
            Role player = GameScope.Instance.Player.Role;
            Role target = Target;
            foreach (IDialogueAction item in register.Items
                         .Where(item => item.VisibleFor(target)))
            {
                unit.ActionList.Add(item);
            }
            service.Panel.SetInteraction(unit);
            service.Panel.SetLeftRole(player);
            service.Panel.SetRightRole(target);
        }

        public override string ShowName => "闲聊";

        public override bool Visible(Role target)
        {
            return true;
        }
    }
}