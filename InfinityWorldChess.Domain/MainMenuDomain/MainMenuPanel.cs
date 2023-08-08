#region

using Secyud.Ugf;
using UnityEngine;

#endregion

namespace InfinityWorldChess.MainMenuDomain
{
	public class MainMenuPanel : MonoBehaviour
	{
		
		public void OnEnterGameClick(bool value)
		{
			
			
		}


		public void OnSettingsClick()
		{
#if UNITY_EDITOR
			Debug.Log("Settings");
#endif
		}

		public void OnExitGameClick()
		{
			U.Get<MainMenuScope>().ExitGame();
		}
	}
}