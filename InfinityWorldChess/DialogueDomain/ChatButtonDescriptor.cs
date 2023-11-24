using System.Linq;
using InfinityWorldChess.DialogueTemplates;
using InfinityWorldChess.GameDomain;
using InfinityWorldChess.RoleAccessors;
using InfinityWorldChess.RoleDomain;
using Secyud.Ugf;
using Secyud.Ugf.TableComponents.ButtonComponents;

namespace InfinityWorldChess.DialogueDomain
{
    public class ChatButtonDescriptor : ButtonDescriptor<Role>
    {
        public override void Invoke()
        {
            DialogueService service = U.Get<DialogueService>();
            service.OpenDialoguePanel();
            ChatRegister register = U.Get<ChatRegister>();
            
            GameScope.Instance.Role.MainOperationRole = Target;
            
            CustomDialogueUnit unit = new()
            {
                Text = "你找我干什么？",
                RoleAccessor = new RoleDirectly
                {
                    Value = Target
                }
            };
            
            foreach (IDialogueFunction item in register.Items)
            {
                item.SetDialogue(unit);
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