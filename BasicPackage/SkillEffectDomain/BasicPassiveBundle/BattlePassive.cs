using InfinityWorldChess.BattleDomain;
using InfinityWorldChess.RoleDomain;
using InfinityWorldChess.SkillDomain;

namespace InfinityWorldChess.SkillEffectDomain.BasicPassiveBundle
{
    public abstract class BattlePassive:IPassiveSkillEffect,IOnBattleRoleInitialize
    {
        public abstract string ShowDescription { get; }
        
        public virtual void Equip( Role role,IPassiveSkill skill)
        {
            role.Buffs.BattleInitializes.Add(this);
        }

        public virtual void UnEquip( Role role,IPassiveSkill skill)
        {
            role.Buffs.BattleInitializes.Remove(this);
        }

        public abstract void OnBattleInitialize(BattleRole chess);
    }
}