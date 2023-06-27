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
				U.T["Gender_" + (property.Female ? "Female" : "Male")],
				property.BirthYear.ToString(),
				U.T[$"{nameof(MonthType)}_{property.BirthMonth}"] + " " +
				U.T[$"{nameof(DayType)}_{property.BirthDay}"] + " " +
				U.T[$"{nameof(HourType)}_{property.BirthHour}"]
			);
		}
	}
}