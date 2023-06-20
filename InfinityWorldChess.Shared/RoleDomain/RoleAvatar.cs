#region

using Secyud.Ugf.Archiving;
using System.IO;

#endregion

namespace InfinityWorldChess.RoleDomain
{
	public class RoleAvatar : IArchivable
	{
		public struct AvatarElement4 : IArchivable
		{
			public int Id;
			public byte X;
			public byte Y;
			public byte Z;
			public byte W;

			public AvatarElement4(int id)
			{
				Id = id;
				X = 0x80;
				Y = 0x80;
				Z = 0x80;
				W = 0x80;
			}

			public AvatarElement4(int id, byte x, byte y, byte z, byte w)
			{
				Id = id;
				X = x;
				Y = y;
				Z = z;
				W = w;
			}

			public void Save(BinaryWriter writer)
			{
				writer.Write(Id);
				writer.Write(X);
				writer.Write(Y);
				writer.Write(Z);
				writer.Write(W);
			}

			public void Load(BinaryReader reader)
			{
				Id = reader.ReadInt32();
				X = reader.ReadByte();
				Y = reader.ReadByte();
				Z = reader.ReadByte();
				W = reader.ReadByte();
			}
		}

		public struct AvatarElement2X : IArchivable
		{
			public int Id1;
			public int Id2;
			public byte X;
			public byte Y;
			public byte Z;
			public byte W;

			public AvatarElement2X(int id1, int id2)
			{
				Id1 = id1;
				Id2 = id2;
				X = 0x80;
				Y = 0x80;
				Z = 0x80;
				W = 0x80;
			}

			public AvatarElement2X(int id1, int id2, byte x, byte y, byte z, byte w)
			{
				Id1 = id1;
				Id2 = id2;
				X = x;
				Y = y;
				Z = z;
				W = w;
			}

			public void Save(BinaryWriter writer)
			{
				writer.Write(Id1);
				writer.Write(Id2);
				writer.Write(X);
				writer.Write(Y);
				writer.Write(Z);
				writer.Write(W);
			}

			public void Load(BinaryReader reader)
			{
				Id1 = reader.ReadInt32();
				Id2 = reader.ReadInt32();
				X = reader.ReadByte();
				Y = reader.ReadByte();
				Z = reader.ReadByte();
				W = reader.ReadByte();
			}
		}


		public struct AvatarElement : IArchivable
		{
			public int Id;

			public AvatarElement(int id)
			{
				Id = id;
			}

			public void Save(BinaryWriter writer)
			{
				writer.Write(Id);
			}

			public void Load(BinaryReader reader)
			{
				Id = reader.ReadInt32();
			}
		}

		public AvatarElement BackItem;
		public AvatarElement BackHair;
		public AvatarElement Body;
		public AvatarElement Head;
		public AvatarElement4 HeadFeature;
		public AvatarElement2X NoseMouth;
		public AvatarElement4 Eye;
		public AvatarElement4 Brow;
		public AvatarElement FrontHair;

		public void Save(BinaryWriter writer)
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

		public void Load(BinaryReader reader)
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