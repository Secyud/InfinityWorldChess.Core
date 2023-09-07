using InfinityWorldChess.BattleDomain;
using InfinityWorldChess.RoleDomain;
using InfinityWorldChess.SkillDomain;

namespace InfinityWorldChess.SkillEffectDomain.BasicPassiveBundle
{
    public class AddProperty:IPassiveSkillEffect,IOnBattleRoleInitialize
    {
        private Role _role;
        private IPassiveSkill _skill;
        private byte _living;
        private byte _kiling;
        private byte _nimble;
        private byte _defend;
        public string ShowDescription => "战斗时提供四维属性加成。";
        public void Equip(IPassiveSkill skill, Role role)
        {
            _skill = skill;
            _role = role;
            role.Buffs.BattleInitializes.Add(this);
        }

        public void UnEquip(IPassiveSkill skill, Role role)
        {
            _skill = skill;
            _role = null;
            role.Buffs.BattleInitializes.Remove(this);
        }

        public void OnBattleInitialize(BattleRole chess)
        {
            Add();
            BattleScope.Instance.Battle.BattleFinishAction += Remove;
        }

        private void Add()
        {
            _living = _skill.Living;
            _kiling = _skill.Kiling;
            _nimble = _skill.Nimble;
            _defend = _skill.Defend;
            _role.BodyPart.Living.MaxValue += _living;
            _role.BodyPart.Kiling.MaxValue += _kiling;
            _role.BodyPart.Nimble.MaxValue += _nimble;
            _role.BodyPart.Defend.MaxValue += _defend;
        }
        private void Remove()
        {
            _role.BodyPart.Living.MaxValue -= _living;
            _role.BodyPart.Kiling.MaxValue -= _kiling;
            _role.BodyPart.Nimble.MaxValue -= _nimble;
            _role.BodyPart.Defend.MaxValue -= _defend;
        }
    }
}