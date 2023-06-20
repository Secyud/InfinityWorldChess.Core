using InfinityWorldChess.RoleDomain;
using Secyud.Ugf.DependencyInjection;

namespace InfinityWorldChess.InteractionDomain
{
    public class InteractionContext:IScoped
    {
        public Role LeftRole { get; set; }
        public Role RightRole { get; set; }
        public RoleInteractionComponent Component { get; internal set; }
    }
}