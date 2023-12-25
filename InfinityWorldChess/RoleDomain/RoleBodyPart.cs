#region

using System;
using System.IO;
using Secyud.Ugf.DataManager;

#endregion

namespace InfinityWorldChess.RoleDomain
{
    public class RoleBodyPart 
    {
        [field: S ] public int MaxValue { get; set; } = 10;

        [field: S] public int RealValue { get; set; } = 10;

        public void ChangeRealValue(int value)
        {
            RealValue = Math.Min(MaxValue, RealValue + value);
        }

        public override string ToString()
        {
            return $"{RealValue:0}/{MaxValue}";
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