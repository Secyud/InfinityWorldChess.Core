#region

using InfinityWorldChess.GlobalDomain;
using Secyud.Ugf;
using Secyud.Ugf.BasicComponents;
using UnityEngine;

#endregion

namespace InfinityWorldChess.RoleDomain
{
	public class BasicForm : MonoBehaviour
	{
		[SerializeField] private SText[] Values;

		public void OnInitialize(Role.BasicProperty property)
		{
			Values.Set(
				property.Name,
				Og.L["Gender_" + (property.Female ? "Female" : "Male")],
				property.BirthYear.ToString(),
				Og.L[$"{nameof(MonthType)}_{property.BirthMonth}"] + " " +
				Og.L[$"{nameof(DayType)}_{property.BirthDay}"] + " " +
				Og.L[$"{nameof(HourType)}_{property.BirthHour}"]
			);
		}
	}
}