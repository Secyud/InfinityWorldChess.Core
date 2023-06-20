#region

using InfinityWorldChess.GlobalDomain;
using Secyud.Ugf.BasicComponents;
using UnityEngine;

#endregion

namespace InfinityWorldChess.RoleDomain
{
	public class BirthEditor : MonoBehaviour
	{
		[SerializeField] private SText[] Tags;

		private Role.BasicProperty _basicProperty;

		public void SetBirthYear(float year)
		{
			_basicProperty.BirthYear = (int)year;
			Tags[0].Translate($"{nameof(YearType)}_{_basicProperty.BirthYear}");
		}

		public void SetBirthMonth(float month)
		{
			_basicProperty.BirthMonth = (byte)month;
			Tags[1].Translate($"{nameof(MonthType)}_{_basicProperty.BirthMonth}");
		}

		public void SetBirthDay(float day)
		{
			_basicProperty.BirthDay = (byte)day;
			Tags[2].Translate($"{nameof(DayType)}_{_basicProperty.BirthDay}");
		}

		public void SetBirthHour(float hour)
		{
			_basicProperty.BirthHour = (byte)hour;
			Tags[3].Translate($"{nameof(HourType)}_{_basicProperty.BirthHour}");
		}

		public void OnInitialize(Role.BasicProperty basicProperty)
		{
			_basicProperty = basicProperty;
		}
	}
}