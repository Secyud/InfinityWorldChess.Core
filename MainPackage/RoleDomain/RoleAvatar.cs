#region

using System.Runtime.InteropServices;
using Secyud.Ugf.Archiving;
using Secyud.Ugf.DataManager;

#endregion

namespace InfinityWorldChess.RoleDomain
{
    [Guid("E4884F79-EF1B-322B-C6BB-629AF7E8A57E")]
    public class AvatarElement : IArchivable
    {
        public static AvatarElement DefaultElement { get; } = new();

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