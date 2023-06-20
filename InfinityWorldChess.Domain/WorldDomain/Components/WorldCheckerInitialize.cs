#region

using UnityEngine;

#endregion

namespace InfinityWorldChess.WorldDomain
{
	public abstract class WorldCheckerInitialize : MonoBehaviour
	{
		public abstract void OnInitialize(WorldChecker checker);
	}
}