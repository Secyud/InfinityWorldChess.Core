using InfinityWorldChess.BattleDomain;
using InfinityWorldChess.RoleDomain;
using InfinityWorldChess.SkillDomain;

namespace InfinityWorldChess.SkillFunctions
{
    public abstract class BattlePassive : IPassiveSkillEffect, IOnBattleRoleInitialize
    {
        public abstract string Description { get; }

        public virtual void Equip(Role role, IPassiveSkill skill)
        {
            role.IdBuffs.BattleInitializes.Add(this);
        }

        public virtual void UnEquip(Role role, IPassiveSkill skill)
        {
            role.IdBuffs.BattleInitializes.Remove(this);
        }

        public abstract void OnBattleInitialize(BattleRole chess);
    }
}