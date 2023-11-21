using InfinityWorldChess.BuffDomain;
using InfinityWorldChess.GlobalDomain;
using InfinityWorldChess.RoleDomain;
using Secyud.Ugf;

namespace InfinityWorldChess.SkillDomain
{
    public interface IPassiveSkillEffect:IHasContent,IEquippable<Role>
    {
    }
}