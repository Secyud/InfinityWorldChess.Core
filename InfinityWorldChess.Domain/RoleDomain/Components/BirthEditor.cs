#region

using InfinityWorldChess.GameDomain;
using Secyud.Ugf;
using Secyud.Ugf.EditorComponents;
using UnityEngine;

#endregion

namespace InfinityWorldChess.RoleDomain.Components
{
    public class BirthEditor : EditorBase<Role.BasicProperty>
    {
        [SerializeField] private EditorEvent<string> YearText;
        [SerializeField] private EditorEvent<string> MonthText;
        [SerializeField] private EditorEvent<string> DayText;
        [SerializeField] private EditorEvent<string> HourText;

        private string BirthYear => $"{nameof(YearType)}_{Property.BirthYear}";

        public void SetBirthYear(float year)
        {
            Property.BirthYear = (int)year;
            YearText.Invoke(U.T[BirthYear]);
        }

        private string BirthMonth => $"{nameof(MonthType)}_{Property.BirthMonth}";

        public void SetBirthMonth(float month)
        {
            Property.BirthMonth = (byte)month;
            MonthText.Invoke(U.T[BirthMonth]);
        }

        private string BirthDay => $"{nameof(DayType)}_{Property.BirthDay}";

        public void SetBirthDay(float day)
        {
            Property.BirthDay = (byte)day;
            DayText.Invoke(U.T[BirthDay]);
        }

        private string BirthHour => $"{nameof(HourType)}_{Property.BirthHour}";

        public void SetBirthHour(float hour)
        {
            Property.BirthHour = (byte)hour;
            HourText.Invoke(U.T[BirthHour]);
        }

        protected override void InitData()
        {
            YearText.Invoke(U.T[BirthYear]);
            MonthText.Invoke(U.T[BirthMonth]);
            DayText.Invoke(U.T[BirthDay]);
            HourText.Invoke(U.T[BirthHour]);
        }
    }
}