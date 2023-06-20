#region

using Secyud.Ugf.Archiving;
using System.Globalization;
using System.IO;

#endregion

namespace InfinityWorldChess.RoleDomain
{
	public partial class Role
	{
		public BasicProperty Basic { get; } = new();

		public class BasicProperty : IArchivable
		{
			// 生辰八字
			public int BirthYear;
			public byte BirthMonth;
			public byte BirthDay;
			public byte BirthHour;
			// 性别
			public bool Female;

			// 名
			public string FirstName;
			// 姓
			public string LastName;
			public string Description;

			public readonly RoleAvatar Avatar = new();

			public string Name
			{
				get =>
					CultureInfo.CurrentCulture.TwoLetterISOLanguageName == "zh"
						? $"{LastName}{FirstName}"
						: $"{FirstName} {LastName}";
				set => LastName = value;
			}

			public void Save(BinaryWriter writer)
			{
				writer.Write(BirthYear);
				writer.Write(BirthMonth);
				writer.Write(BirthDay);
				writer.Write(BirthHour);
				writer.Write(Female);
				writer.Write(FirstName);
				writer.Write(LastName);
				writer.Write(Description??string.Empty);
				Avatar.Save(writer);
			}

			public void Load(BinaryReader reader)
			{
				BirthYear = reader.ReadInt32();
				BirthMonth = reader.ReadByte();
				BirthDay = reader.ReadByte();
				BirthHour = reader.ReadByte();
				Female = reader.ReadBoolean();
				FirstName = reader.ReadString();
				LastName = reader.ReadString();
				Description = reader.ReadString();
				Avatar.Load(reader);
			}
		}
	}
}