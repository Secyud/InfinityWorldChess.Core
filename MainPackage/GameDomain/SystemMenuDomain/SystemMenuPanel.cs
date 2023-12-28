using System;
using InfinityWorldChess.MainMenuDomain;
using InfinityWorldChess.Ugf;
using Secyud.Ugf;
using Secyud.Ugf.Archiving;
using Secyud.Ugf.FunctionalComponents;
using UnityEngine;

namespace InfinityWorldChess.GameDomain.SystemMenuDomain
{
	public class SystemMenuPanel : MonoBehaviour
	{
		public void ContinueGame()
		{
			GameScope.Instance.CloseSystemMenu();
		}

		public void ReturnMainMenu()
		{
			"未保存的进度将会丢失".CreateEnsureFloatingOnCenter(() =>
			{
				GameScope.Instance.CloseSystemMenu();
				U.M.DestroyScope<GameScope>();
				U.M.CreateScope<MainMenuScope>();
			});
		}

		public void Save()
		{
			SaveGame(() => "保存完成".CreateTipFloatingOnCenter());
		}

		private void SaveGame(Action endAction)
		{
			LoadingPanel loading = U.Get<IwcAssets>().LoadingPanelCircle.Instantiate();
			loading.DestroyAction = endAction;
			
			
			U.Factory.SaveGame();
		}

		public void ExitGame()
		{
			"未保存的进度将会丢失".CreateEnsureFloatingOnCenter(GameScope.Instance.ExitGame);
		}
	}
}