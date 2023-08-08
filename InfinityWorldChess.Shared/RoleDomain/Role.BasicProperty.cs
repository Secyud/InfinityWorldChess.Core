#region

using System;
using System.Collections.Generic;
using Secyud.Ugf.Archiving;
using System.Globalization;
using Secyud.Ugf.DataManager;

#endregion

namespace InfinityWorldChess.RoleDomain
{
    public partial class Role
    {
        public BasicProperty Basic { get; } = new();

        public class BasicProperty : AutoArchiving
        {
            [S(ID = 0, DataType = DataType.Ignored)]
            public readonly AvatarElement[] Avatar =
                new AvatarElement[SharedConsts.AvatarElementCount];

            [S(ID = 1)] public int BirthYear;
            [S(ID = 2)] public byte BirthMonth;
            [S(ID = 3)] public byte BirthDay;
            [S(ID = 4)] public byte BirthHour;

            [S(ID = 5)] public bool Female;

            // 名
            [S(ID = 6)] public string FirstName = string.Empty;

            // 姓
            [S(ID = 7)] public string LastName = string.Empty;
            [S(ID = 8)] public string Description;

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
                foreach (AvatarElement e in Avatar)
                {
                    e.Save(writer);
                }
            }

            public override void Load(IArchiveReader reader)
            {
                base.Load(reader);
                foreach (AvatarElement e in Avatar)
                {
                    e.Load(reader);
                }
            }
        }
    }
}