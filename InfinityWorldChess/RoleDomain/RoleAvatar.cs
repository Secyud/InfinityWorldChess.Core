#region

using Secyud.Ugf.Archiving;
using Secyud.Ugf.DataManager;

#endregion

namespace InfinityWorldChess.RoleDomain
{
    public class AvatarElement : IArchivable
    {
        [S] public int Id;
        [S] public byte PositionX = 0x80;
        [S] public byte PositionY = 0x80;
        [S] public byte Scale = 0x80;
        [S] public byte Rotation = 0x80;


        public void Save(IArchiveWriter writer)
        {
            writer.Write(Id);
            writer.Write(PositionX);
            writer.Write(PositionY);
            writer.Write(Scale);
            writer.Write(Rotation);
        }

        public void Load(IArchiveReader reader)
        {
            Id = reader.ReadInt32();
            PositionX = reader.ReadByte();
            PositionY = reader.ReadByte();
            Scale = reader.ReadByte();
            Rotation = reader.ReadByte();
        }
    }
}