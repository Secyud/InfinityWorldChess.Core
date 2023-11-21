using InfinityWorldChess.BattleDomain;
using InfinityWorldChess.RoleDomain;
using InfinityWorldChess.SkillDomain;

namespace InfinityWorldChess.SkillFunctions
{
    public class AddProperty:BattlePassive
    {
        private Role _role;
        private IPassiveSkill _skill;
        private byte _living;
        private byte _kiling;
        private byte _nimble;
        private byte _defend;
        public override string Description => "战斗时提供四维属性加成。";
        public override void Equip( Role role,IPassiveSkill skill)
        {
            base.Equip(role,skill);
            _skill = skill;
            _role = role;
        }

        public override void UnEquip( Role role,IPassiveSkill skill)
        {
            base.UnEquip(role,skill);
            _skill = skill;
            _role = null;
        }

        public override void OnBattleInitialize(BattleRole chess)
        {
            Add();
            BattleScope.Instance.Context.BattleFinishAction += Remove;
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