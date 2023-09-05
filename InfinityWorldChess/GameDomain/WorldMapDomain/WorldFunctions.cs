#region

using UnityEngine;

#endregion

namespace InfinityWorldChess.GameDomain.WorldMapDomain
{
	public class WorldFunctions : MonoBehaviour
	{
		public void CallGameMenu()
		{
			GameScope.Instance.OpenGameMenu();
		}
		
		public void ResetCamera()
		{
			WorldGameContext.Map.MapCamera.SetTargetPosition(
				GameScope.Instance.Player.Unit.transform.position
			);
		}
		
		public void OpenSystemMenu()
		{
			GameScope.Instance.OpenSystemMenu();
		}
	}
}