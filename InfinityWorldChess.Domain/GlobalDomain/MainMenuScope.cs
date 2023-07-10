using Secyud.Ugf.AssetLoading;
using Secyud.Ugf.DependencyInjection;
using UnityEditor;

namespace InfinityWorldChess.GlobalDomain
{
    [Registry(DependScope = typeof(GlobalScope))]
    public class MainMenuScope:DependencyScopeProvider
    {
        public static IMonoContainer<MainMenuComponent> MainMenu;
        
        public MainMenuScope(IwcAb ab) 
        {
            MainMenu ??= MonoContainer<MainMenuComponent>.Create(ab);
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