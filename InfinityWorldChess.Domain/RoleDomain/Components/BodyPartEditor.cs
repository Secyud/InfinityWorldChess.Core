#region

using System;
using Secyud.Ugf.EditorComponents;
using UnityEngine;

#endregion

namespace InfinityWorldChess.RoleDomain.Components
{
    public class BodyPartEditor : EditorBase<Role.BodyPartProperty>
    {
        [SerializeField] private EditorEvent<string> Living;
        [SerializeField] private EditorEvent<string> Kiling;
        [SerializeField] private EditorEvent<string> Nimble;
        [SerializeField] private EditorEvent<string> Defend;

        public void SetMaxLiving(float value)
        {
            Property.Living.MaxValue = Convert.ToInt32(value);
            Living.Invoke(Property.Living.ToString());
        }

        public void SetLiving(float value)
        {
            Property.Living.RealValue = Convert.ToInt32(value);
            Living.Invoke(Property.Living.ToString());
        }

        public void SetMaxKiling(float value)
        {
            Property.Kiling.MaxValue = Convert.ToInt32(value);
            Kiling.Invoke(Property.Kiling.ToString());
        }

        public void SetKiling(float value)
        {
            Property.Kiling.RealValue = Convert.ToInt32(value);
            Kiling.Invoke(Property.Kiling.ToString());
        }

        public void SetMaxNimble(float value)
        {
            Property.Nimble.MaxValue = Convert.ToInt32(value);
            Nimble.Invoke(Property.Nimble.ToString());
        }

        public void SetNimble(float value)
        {
            Property.Nimble.RealValue = Convert.ToInt32(value);
            Nimble.Invoke(Property.Nimble.ToString());
        }

        public void SetMaxDefend(float value)
        {
            Property.Defend.MaxValue = Convert.ToInt32(value);
            Defend.Invoke(Property.Defend.ToString());
        }

        public void SetDefend(float value)
        {
            Property.Defend.RealValue = Convert.ToInt32(value);
            Defend.Invoke(Property.Defend.ToString());
        }

        protected override void InitData()
        {
            Living.Invoke(Property.Living.ToString());
            Kiling.Invoke(Property.Kiling.ToString());
            Nimble.Invoke(Property.Nimble.ToString());
            Defend.Invoke(Property.Defend.ToString());
        }
    }
}