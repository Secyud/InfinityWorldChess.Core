using Secyud.Ugf.AssetLoading;
using Secyud.Ugf.DependencyInjection;
using UnityEditor;

namespace InfinityWorldChess.GlobalDomain
{
    [DependScope(typeof(GlobalScope))]
    public class MainMenuScope:DependencyScope
    {
        public static IMonoContainer<MainMenuComponent> MainMenu;
        
        public MainMenuScope(DependencyManager dependencyManager,IwcAb ab) : base(dependencyManager)
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