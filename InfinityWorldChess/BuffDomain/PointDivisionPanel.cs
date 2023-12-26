using System;
using System.Linq;
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
        private float[] _proportion;

        private void Awake()
        {
            _properties = new byte[4];
            _proportion = new float[] { 1, 1, 1, 1 };
        }

        public void SetLiving(float value)
        {
            _proportion[0] = value;
            SetOriginValue();
        }

        public void SetKiling(float value)
        {
            _proportion[1] = value;
            SetOriginValue();
        }

        public void SetNimble(float value)
        {
            _proportion[2] = value;
            SetOriginValue();
        }

        public void SetDefend(float value)
        {
            _proportion[3] = value;
            SetOriginValue();
        }

        private void SetOriginValue()
        {
            Role role = GameScope.Instance.Role.MainOperationRole;
            byte total = (byte)(role.Basic.Level / 0x80000);

            float sumProportion = _proportion.Sum(u => u);

            for (int i = 0; i < 4; i++)
            {
                byte property = (byte)Math.Round(total * _proportion[i] / sumProportion);
                _properties[i] = property;
            }

            byte result = (byte)_properties.Sum(u => u);

            if (result != total)
            {
                int index = 0;
                if (result > total)
                {
                    for (byte i = 1; i < 4; i++)
                    {
                        if (_properties[i] > _properties[index])
                        {
                            index = i;
                        }
                    }
                }
                else if (result < total)
                {
                    for (byte i = 1; i < 4; i++)
                    {
                        if (_properties[i] < _properties[index])
                        {
                            index = i;
                        }
                    }
                }
                _properties[index] = (byte)(_properties[index] - result + total);
            }

            for (int i = 0; i < 4; i++)
            {
                Properties[i].Invoke(_properties[i]);
            }
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
            _proportion[0] = Property.Living + 1;
            _proportion[1] = Property.Kiling + 1;
            _proportion[2] = Property.Nimble + 1;
            _proportion[3] = Property.Defend + 1;
            Destroy(gameObject);
        }

        protected override void InitData()
        {
            SetOriginValue();
        }
    }
}