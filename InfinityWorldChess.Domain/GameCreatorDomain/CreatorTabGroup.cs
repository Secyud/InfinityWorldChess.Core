using InfinityWorldChess.MainMenuDomain;
using Secyud.Ugf;
using Secyud.Ugf.TabComponents;
using UnityEngine;

namespace InfinityWorldChess.GameCreatorDomain
{
    public class CreatorTabGroup:TabGroup<CreatorTabService, CreatorTabItem>
    {
        public void EnterGame()
        {
            var validator = U.Get<CreatorValidateService>();
            validator.Refresh();
            if (!validator.Valid)
                return;

            IwcAb.Instance.LoadingPanelInk.Instantiate();

            U.Factory.InitializeGame();
        }

        public void ReturnMainMenu()
        {
            U.Factory.Application.DependencyManager.CreateScope<MainMenuScope>();
            U.Factory.Application.DependencyManager.DestroyScope<GameCreatorScope>();
        }
    }
}