#region

using InfinityWorldChess.BuffDomain;
using InfinityWorldChess.ItemDomain.FoodDomain;
using InfinityWorldChess.SkillDomain;
using Secyud.Ugf.Archiving;

#endregion

namespace InfinityWorldChess.Ugf
{
    public static class BpArchiveExtension
    {
        public static void SaveProperty(this IAttachProperty property, IArchiveWriter writer)
        {
            writer.Write(property.Living);
            writer.Write(property.Kiling);
            writer.Write(property.Nimble);
            writer.Write(property.Defend);
        }
        public static void LoadProperty(this IAttachProperty property, IArchiveReader reader)
        {
            property.Living = reader.ReadByte();
            property.Kiling = reader.ReadByte();
            property.Nimble = reader.ReadByte();
            property.Defend = reader.ReadByte();
        }
    }
}