using InfinityWorldChess.GlobalDomain;
using InfinityWorldChess.RoleDomain;
using Secyud.Ugf.DataManager;

namespace InfinityWorldChess.SkillDomain
{
    public class PassiveSkillEffectDelegate:IPassiveSkillEffect
    {
        public string ShowDescription => Effect?.ShowDescription;
        
        [field:S] public ICanBeEquipped Effect { get; set; }

        public void Equip(IPassiveSkill skill, Role role)
        {
            Effect?.Equip(role);
        }

        public void UnEquip(IPassiveSkill skill, Role role)
        {
            Effect?.UnEquip(role);
        }
    }
}