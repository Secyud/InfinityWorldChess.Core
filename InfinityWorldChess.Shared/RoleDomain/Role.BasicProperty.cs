#region

using Secyud.Ugf.Archiving;
using System.Globalization;
using Secyud.Ugf.DataManager;

#endregion

namespace InfinityWorldChess.RoleDomain
{
    public partial class Role
    {
        public BasicProperty Basic { get; } = new();

        public class BasicProperty : DataObject
        {
            [S(0, ignore: true)] public readonly RoleAvatar Avatar = new();
            // 生辰八字
            [S(1)] public int BirthYear;
            [S(2)] public byte BirthMonth;
            [S(3)] public byte BirthDay;
            [S(4)] public byte BirthHour;
            // 性别
            [S(5)] public bool Female;
            // 名
            [S(6)] public string FirstName;
            // 姓
            [S(7)] public string LastName;
            [S(8)] public string Description;

            public string Name
            {
                get =>
                    CultureInfo.CurrentCulture.TwoLetterISOLanguageName == "zh"
                        ? $"{LastName}{FirstName}"
                        : $"{FirstName} {LastName}";
                set => LastName = value;
            }

            public override void Save(IArchiveWriter writer)
            {
                base.Save(writer);
                Avatar.Save(writer);
            }

            public override void Load(IArchiveReader reader)
            {
                base.Load(reader);
                Avatar.Load(reader);
            }
        }
    }
}