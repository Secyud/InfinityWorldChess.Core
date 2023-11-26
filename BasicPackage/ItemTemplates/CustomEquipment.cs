using Secyud.Ugf.Archiving;

namespace InfinityWorldChess.ItemTemplates
{
    public class CustomEquipment:Equipment
    {
        public override void Save(IArchiveWriter writer)
        {
            writer.Write(TypeCode);
            writer.Write(Location);
            writer.Write(Living);
            writer.Write(Kiling);
            writer.Write(Nimble);
            writer.Write(Defend);
            
        }

        public override void Load(IArchiveReader reader)
        {
            TypeCode = reader.ReadByte();
            Location = reader.ReadByte();
            Living = reader.ReadByte();
            Kiling = reader.ReadByte();
            Nimble = reader.ReadByte();
            Defend = reader.ReadByte();
        }

    }
}