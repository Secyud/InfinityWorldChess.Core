#region

using Secyud.Ugf.Archiving;
using System.Globalization;

#endregion

namespace InfinityWorldChess.RoleDomain
{
    public partial class Role
    {
        public BasicProperty Basic { get; } = new();

        public class BasicProperty : IArchivable
        {
            private int _level;

            public AvatarElement[] Avatar { get; } =
                new AvatarElement[IWCC.AvatarElementCount];

            public int BirthYear { get; set; }
            public byte BirthMonth { get; set; }
            public byte BirthDay { get; set; }
            public byte BirthHour { get; set; }

            public bool Female { get; set; }

            public int Level
            {
                get => _level;
                set
                {
                    if (value >= 0)
                    {
                        _level = value;
                    }
                }
            }

            // 名
            public string FirstName { get; set; } = string.Empty;

            // 姓
            public string LastName { get; set; } = string.Empty;
            public string Description { get; set; }

            public BasicProperty()
            {
                for (int i = 0; i < IWCC.AvatarElementCount; i++)
                {
                    Avatar[i] = new AvatarElement();
                }
            }

            public string Name =>
                CultureInfo.CurrentCulture.TwoLetterISOLanguageName == "zh"
                    ? $"{LastName}{FirstName}"
                    : $"{FirstName} {LastName}";

            public void Save(IArchiveWriter writer)
            {
                writer.Write(BirthYear);
                writer.Write(BirthMonth);
                writer.Write(BirthDay);
                writer.Write(BirthHour);
                writer.Write(Female);
                writer.Write(Level);
                writer.Write(FirstName);
                writer.Write(LastName);
                writer.Write(Description);

                foreach (AvatarElement e in Avatar)
                {
                    e.Save(writer);
                }
            }

            public void Load(IArchiveReader reader)
            {
                BirthYear = reader.ReadInt32();
                BirthMonth = reader.ReadByte();
                BirthDay = reader.ReadByte();
                BirthHour = reader.ReadByte();
                Female = reader.ReadBoolean();
                Level = reader.ReadInt32();
                FirstName = reader.ReadString();
                LastName = reader.ReadString();
                Description = reader.ReadString();
                foreach (AvatarElement e in Avatar)
                {
                    e.Load(reader);
                }
            }
        }
    }
}