#region

using Secyud.Ugf.BasicComponents;
using UnityEngine;

#endregion

namespace InfinityWorldChess.BattleDomain
{
	public class BattleCheckerMessage : MonoBehaviour
	{
		[SerializeField] private SText Position;

		public void OnInitialize(BattleChecker message)
		{
			Position.Set(message?.Cell.Coordinates.ToString()??"");
		}
	}
}