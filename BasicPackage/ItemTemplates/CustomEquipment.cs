using InfinityWorldChess.Ugf;
using Secyud.Ugf.Archiving;

namespace InfinityWorldChess.ItemTemplates
{
    public class CustomEquipment:Equipment
    {
        public override void Save(IArchiveWriter writer)
        {
            writer.Write(TypeCode);
            writer.Write(Location);
            this.SaveProperty(writer);
            SaveEffects(writer);
        }

        public override void Load(IArchiveReader reader)
        {
            TypeCode = reader.ReadByte();
            Location = reader.ReadByte();
            this.LoadProperty(reader);
            LoadEffects(reader);
        }

    }
}