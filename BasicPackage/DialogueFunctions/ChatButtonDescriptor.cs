using InfinityWorldChess.DialogueDomain;
using InfinityWorldChess.GameDomain;
using InfinityWorldChess.RoleAccessors;
using InfinityWorldChess.RoleDomain;
using Secyud.Ugf;
using Secyud.Ugf.TableComponents.ButtonComponents;

namespace InfinityWorldChess.DialogueFunctions
{
    public class ChatButtonDescriptor : ButtonDescriptor<Role>
    {
        public override void Invoke()
        {
            DialogueService service = U.Get<DialogueService>();
            ChatRegister register = U.Get<ChatRegister>();
            
            GameScope.Instance.Role.MainOperationRole = Target;
            
            DialogueUnit unit = new()
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