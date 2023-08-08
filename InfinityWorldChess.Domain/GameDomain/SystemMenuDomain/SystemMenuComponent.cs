using System;
using InfinityWorldChess.MainMenuDomain;
using InfinityWorldChess.Ugf;
using Secyud.Ugf;
using Secyud.Ugf.FunctionalComponents;
using UnityEngine;

namespace InfinityWorldChess.GameDomain.SystemMenuDomain
{
	public class SystemMenuComponent : MonoBehaviour
	{
		public void ContinueGame()
		{
			GameScope.CloseSystemMenu();
		}

		public void ReturnMainMenu()
		{
			"未保存的进度将会丢失".CreateEnsureFloatingOnCenter(() =>
			{
				GameScope.CloseSystemMenu();
				U.Factory.GameShutdown();
				U.Factory.Application.DependencyManager.CreateScope<MainMenuScope>();
			});
		}

		public void Save()
		{
			SaveGame(() => "保存完成".CreateTipFloatingOnCenter());
		}

		private void SaveGame(Action endAction)
		{
			LoadingPanel loading = IwcAb.Instance.LoadingPanelCircle.Instantiate();
			loading.DestroyAction = endAction;
			
			
			U.Factory.SaveGame();
		}

		public void SaveAndExit()
		{
			"未保存的进度将会丢失".CreateEnsureFloatingOnCenter(GameScope.ExitGame);
		}
	}
}