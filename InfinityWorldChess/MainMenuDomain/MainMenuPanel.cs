#region

using InfinityWorldChess.ArchivingDomain;
using Secyud.Ugf;
using UnityEngine;

#endregion

namespace InfinityWorldChess.MainMenuDomain
{
	public class MainMenuPanel : MonoBehaviour
	{
		
		public void OnEnterGameClick()
		{
			ArchivingScope.Instance.OpenGameLoadPanel();
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