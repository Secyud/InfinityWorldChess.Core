using InfinityWorldChess.RoleDomain;
using Secyud.Ugf;
using Secyud.Ugf.DataManager;

namespace InfinityWorldChess.DialogueTemplates
{
    public class DialogueTuple
    {
        [field:S] public string Text { get; set; }
        [field:S] public IObjectAccessor<Role> RoleAccessor { get; set; }
    }
}