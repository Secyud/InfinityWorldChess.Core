#region

using System;
using System.IO;
using System.Runtime.InteropServices;
using Secyud.Ugf.DataManager;

#endregion

namespace InfinityWorldChess.RoleDomain
{
    [Guid("4D142541-D579-A4E7-EED5-C8429AFA44EC")]
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