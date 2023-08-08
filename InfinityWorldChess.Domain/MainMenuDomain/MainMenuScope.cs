using InfinityWorldChess.GlobalDomain;
using Secyud.Ugf.AssetComponents;
using Secyud.Ugf.DependencyInjection;
using UnityEditor;

namespace InfinityWorldChess.MainMenuDomain
{
    [Registry(DependScope = typeof(GlobalScope))]
    public class MainMenuScope:DependencyScopeProvider
    {
        public static IMonoContainer<MainMenuPanel> MainMenu;
        
        public MainMenuScope(IwcAb ab) 
        {
            MainMenu ??= MonoContainer<MainMenuPanel>.Create(ab);
            MainMenu.Create();
        }

        public override void Dispose()
        {
            MainMenu.Destroy();
        }


        public void ExitGame()
        {
#if UNITY_EDITOR
            EditorApplication.isPlaying = false;
#else
			UnityEngine.Application.Quit();
#endif
        }
    }
}