using InfinityWorldChess.RoleDomain;
using Secyud.Ugf;

namespace InfinityWorldChess.SkillDomain
{
    public interface IPassiveSkillEffect:IHasDescription
    {
        void Equip(IPassiveSkill skill,Role role);
        void UnEquip(IPassiveSkill skill,Role role);
    }
}