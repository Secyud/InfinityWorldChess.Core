#region

using System;
using System.IO;
using Secyud.Ugf.DataManager;

#endregion

namespace InfinityWorldChess.RoleDomain
{
    public class RoleBodyPart : DataObject
    {
        [S(ID =0)] private float _realValue;

        [field: S(ID =1)]
        public int MaxValue { get; set; }

        public float RealValue
        {
            get => _realValue;
            set => _realValue = Math.Min(MaxValue, value);
        }


        public override string ToString()
        {
            return $"{RealValue}/{MaxValue}";
        }

        public void Save(BinaryWriter writer)
        {
            writer.Write(MaxValue);
            writer.Write(RealValue);
        }

        public void Load(BinaryReader reader)
        {
            MaxValue = reader.ReadInt32();
            RealValue = reader.ReadInt32();
        }
    }
}