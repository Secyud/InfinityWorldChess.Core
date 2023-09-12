using InfinityWorldChess.MainMenuDomain;
using Secyud.Ugf;
using Secyud.Ugf.TabComponents;
using UnityEngine;

namespace InfinityWorldChess.GameCreatorDomain
{
    public class CreatorTabGroup:TabGroup
    {
        public void EnterGame()
        {
            CreatorValidateService validator = U.Get<CreatorValidateService>();
            
            if (!validator.CheckValid())
                return;

            IwcAssets.Instance.LoadingPanelInk.Instantiate();

            U.Factory.InitializeGame();
        }

        public void ReturnMainMenu()
        {
            U.M.CreateScope<MainMenuScope>();
            U.M.DestroyScope<GameCreatorScope>();
        }

        protected override TabService Service => GameCreatorScope.Instance.Get<CreatorTabService>();
    }
}