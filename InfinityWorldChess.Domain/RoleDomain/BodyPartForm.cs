#region

using Secyud.Ugf.BasicComponents;
using UnityEngine;

#endregion

namespace InfinityWorldChess.RoleDomain
{
	public class BodyPartForm : MonoBehaviour
	{
		[SerializeField] private SText[] Values;

		public void OnInitialize(Role.BodyPartProperty property)
		{
			Values.Set(
				property[BodyType.Living].ToString(),
				property[BodyType.Kiling].ToString(),
				property[BodyType.Nimble].ToString(),
				property[BodyType.Defend].ToString()
			);
		}
	}
}