using System;
using InfinityWorldChess.GlobalDomain;
using InfinityWorldChess.Ugf;
using Secyud.Ugf;
using Secyud.Ugf.FunctionalComponents;
using Secyud.Ugf.Modularity;
using UnityEngine;

namespace InfinityWorldChess.PlayerDomain
{
	public class SystemMenuComponent : MonoBehaviour
	{
		public void ContinueGame()
		{
			GameScope.OnSystemMenuShutdown();
		}

		public void ReturnMainMenu()
		{
			"未保存的进度将会丢失".CreateEnsureFloatingOnCenter(() =>
			{
				GameScope.OnSystemMenuShutdown();
				UgfApplicationFactory<StartupModule>.GameShutdown();
				Og.ScopeFactory.CreateScope<MainMenuScope>();
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
			UgfApplicationFactory<StartupModule>.GameSaving();
		}

		public void SaveAndExit()
		{
			"未保存的进度将会丢失".CreateEnsureFloatingOnCenter(GameScope.ExitGame);
		}
	}
}