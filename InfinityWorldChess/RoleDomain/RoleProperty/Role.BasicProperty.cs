#region

using Secyud.Ugf.Archiving;
using System.Globalization;
using Secyud.Ugf.DataManager;

#endregion

namespace InfinityWorldChess.RoleDomain
{
    public partial class Role
    {
        [field:S] public BasicProperty Basic { get; } = new();

        public class BasicProperty : IArchivable
        {
            [S] public readonly AvatarElement[] Avatar =
                new AvatarElement[SharedConsts.AvatarElementCount];

            [S] public int BirthYear;
            [S] public byte BirthMonth;
            [S] public byte BirthDay;
            [S] public byte BirthHour;

            [S] public bool Female;

            // 名
            [S] public string FirstName = string.Empty;

            // 姓
            [S] public string LastName = string.Empty;
            [S] public string Description;

            public BasicProperty()
            {
                for (int i = 0; i < SharedConsts.AvatarElementCount; i++)
                {
                    Avatar[i] = new AvatarElement();
                }
            }

            public string Name
            {
                get =>
                    CultureInfo.CurrentCulture.TwoLetterISOLanguageName == "zh"
                        ? $"{LastName}{FirstName}"
                        : $"{FirstName} {LastName}";
                set => LastName = value;
            }

            public void Save(IArchiveWriter writer)
            {
                writer.Write(BirthYear);
                writer.Write(BirthMonth);
                writer.Write(BirthDay);
                writer.Write(BirthHour);
                writer.Write(Female);
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