#region

using Secyud.Ugf.Archiving;
using Secyud.Ugf.DataManager;

#endregion

namespace InfinityWorldChess.RoleDomain
{
    public class RoleAvatar : IArchivable
    {
        public class AvatarElement4 : IArchivable
        {
            [S(ID = 0)]public int Id;
            [S(ID = 1)]public byte X= 0x80;
            [S(ID = 2)]public byte Y= 0x80;
            [S(ID = 3)]public byte Z= 0x80;
            [S(ID = 4)]public byte W= 0x80;


            public void Save(IArchiveWriter writer)
            {
                writer.Write(Id);
                writer.Write(X);
                writer.Write(Y);
                writer.Write(Z);
                writer.Write(W);
            }

            public void Load(IArchiveReader reader)
            {
                Id = reader.ReadInt32();
                X = reader.ReadByte();
                Y = reader.ReadByte();
                Z = reader.ReadByte();
                W = reader.ReadByte();
            }
        }
        public class AvatarElement2X : IArchivable
        {
            [S(ID = 0)]public int Id1;
            [S(ID = 1)]public int Id2;
            [S(ID = 2)]public byte X= 0x80;
            [S(ID = 3)]public byte Y= 0x80;
            [S(ID = 4)]public byte Z= 0x80;
            [S(ID = 5)]public byte W= 0x80;

            public void Save(IArchiveWriter writer)
            {
                writer.Write(Id1);
                writer.Write(Id2);
                writer.Write(X);
                writer.Write(Y);
                writer.Write(Z);
                writer.Write(W);
            }

            public void Load(IArchiveReader reader)
            {
                Id1 = reader.ReadInt32();
                Id2 = reader.ReadInt32();
                X = reader.ReadByte();
                Y = reader.ReadByte();
                Z = reader.ReadByte();
                W = reader.ReadByte();
            }
        }
        public class AvatarElement : IArchivable
        {
            [S(ID = 0)]public int Id;

            public void Save(IArchiveWriter writer)
            {
                writer.Write(Id);
            }

            public void Load(IArchiveReader reader)
            {
                Id = reader.ReadInt32();
            }
        }

        [S(ID = 0)]public AvatarElement BackItem = new();
        [S(ID = 1)]public AvatarElement BackHair;
        [S(ID = 2)]public AvatarElement Body;
        [S(ID = 3)]public AvatarElement Head;
        [S(ID = 4)]public AvatarElement4 HeadFeature;
        [S(ID = 5)]public AvatarElement2X NoseMouth;
        [S(ID = 6)]public AvatarElement4 Eye;
        [S(ID = 7)]public AvatarElement4 Brow;
        [S(ID = 8)]public AvatarElement FrontHair;

        public void Save(IArchiveWriter writer)
        {
            BackItem.Save(writer);
            BackHair.Save(writer);
            Body.Save(writer);
            Head.Save(writer);
            HeadFeature.Save(writer);
            NoseMouth.Save(writer);
            Eye.Save(writer);
            Brow.Save(writer);
            FrontHair.Save(writer);
        }

        public void Load(IArchiveReader reader)
        {
            BackItem.Load(reader);
            BackHair.Load(reader);
            Body.Load(reader);
            Head.Load(reader);
            HeadFeature.Load(reader);
            NoseMouth.Load(reader);
            Eye.Load(reader);
            Brow.Load(reader);
            FrontHair.Load(reader);
        }
    }
}