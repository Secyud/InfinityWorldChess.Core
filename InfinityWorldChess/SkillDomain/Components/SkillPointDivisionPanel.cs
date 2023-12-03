using System;
using InfinityWorldChess.GameDomain;
using Secyud.Ugf.EditorComponents;
using UnityEngine;

namespace InfinityWorldChess.SkillDomain
{
    public class SkillPointDivisionPanel : EditorBase<ISkill>
    {
        [SerializeField] public EditorEvent<byte> Living;
        [SerializeField] public EditorEvent<byte> Kiling;
        [SerializeField] public EditorEvent<byte> Nimble;
        [SerializeField] public EditorEvent<byte> Defend;

        public void SetLiving(byte value)
        {
            Property.Living = SetOriginValue(value, Property.Living);
        }

        public void SetKiling(byte value)
        {
            Property.Kiling = SetOriginValue(value, Property.Kiling);
        }

        public void SetNimble(byte value)
        {
            Property.Nimble = SetOriginValue(value, Property.Nimble);
        }

        public void SetDefend(byte value)
        {
            Property.Defend = SetOriginValue(value, Property.Defend);
        }

        private byte SetOriginValue(byte value, byte origin)
        {
            var role = GameScope.Instance.Role.MainOperationRole;
            byte total = (byte)( role.Basic.Level / 0x800000);
            byte current = (byte)(Property.Defend + Property.Living +
                                  Property.Nimble + Property.Kiling);

            return (byte)Math.Min(total - current + origin, value);
        }

        protected override void InitData()
        {
            Living.Invoke(Property.Living);
            Kiling.Invoke(Property.Kiling);
            Nimble.Invoke(Property.Nimble);
            Defend.Invoke(Property.Defend);
        }
    }
}