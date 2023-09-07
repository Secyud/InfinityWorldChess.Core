using InfinityWorldChess.RoleDomain;
using Secyud.Ugf;

namespace InfinityWorldChess.SkillDomain
{
    public interface IPassiveSkillEffect:IHasDescription
    {
        void Equip(Role role,IPassiveSkill skill = null);
        void UnEquip(Role role,IPassiveSkill skill = null);
    }
}