#region

using Secyud.Ugf.Archiving;
using System.Globalization;
using Secyud.Ugf;
using Secyud.Ugf.DataManager;

#endregion

namespace InfinityWorldChess.RoleDomain
{
    public partial class Role
    {
        public BasicProperty Basic { get; } = new();

        public class BasicProperty :IArchivable
        {
            public readonly AvatarElement[] Avatar =
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

            public  void Save(IArchiveWriter writer)
            {
                U.AutoSaveObject(this,writer);
                foreach (AvatarElement e in Avatar)
                {
                    e.Save(writer);
                }
            }

            public  void Load(IArchiveReader reader)
            {
                U.AutoLoadObject(this,reader);
                foreach (AvatarElement e in Avatar)
                {
                    e.Load(reader);
                }
            }
        }
    }
}