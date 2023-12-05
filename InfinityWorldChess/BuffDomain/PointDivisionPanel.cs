using System;
using InfinityWorldChess.GameDomain;
using InfinityWorldChess.RoleDomain;
using Secyud.Ugf.EditorComponents;
using UnityEngine;

namespace InfinityWorldChess.BuffDomain
{
    public class PointDivisionPanel : EditorBase<IAttachProperty>
    {
        [SerializeField] public EditorEvent<float>[] Properties;

        private byte[] _properties;

        private void Awake()
        {
            _properties = new byte[4];
        }

        public void SetLiving(float value)
        {
            SetOriginValue(value, 0);
        }

        public void SetKiling(float value)
        {
            SetOriginValue(value, 1);
        }

        public void SetNimble(float value)
        {
            SetOriginValue(value, 2);
        }

        public void SetDefend(float value)
        {
            SetOriginValue(value, 3);
        }

        private void SetOriginValue(float value, int t)
        {
            Role role = GameScope.Instance.Role.MainOperationRole;
            byte total = (byte)(role.Basic.Level / 0x800000);
            byte current = (byte)(Property.Defend + Property.Living +
                                  Property.Nimble + Property.Kiling);

            _properties[t] = (byte)Math.Min(total - current + _properties[t], value);
            Properties[t].Invoke(_properties[t]);
        }

        public void Ensure()
        {
            Property.Living = _properties[0];
            Property.Kiling = _properties[1];
            Property.Nimble = _properties[2];
            Property.Defend = _properties[3];
            Cancel();
        }

        public void Cancel()
        {
            Destroy(gameObject);
        }

        protected override void InitData()
        {
            SetOriginValue(Property.Living, 0);
            SetOriginValue(Property.Kiling, 1);
            SetOriginValue(Property.Nimble, 2);
            SetOriginValue(Property.Defend, 3);
        }
    }
}