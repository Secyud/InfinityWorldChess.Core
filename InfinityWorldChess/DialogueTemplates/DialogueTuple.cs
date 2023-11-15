using InfinityWorldChess.RoleDomain;
using Secyud.Ugf;
using Secyud.Ugf.DataManager;

namespace InfinityWorldChess.DialogueTemplates
{
    public class DialogueTuple
    {
        [field:S(0)] public string Text { get; set; }
        [field:S(1)] public IObjectAccessor<Role> RoleAccessor { get; set; }
    }
}