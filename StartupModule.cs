#region

using InfinityWorldChess;
using InfinityWorldChess.ArchivingDomain;
using InfinityWorldChess.GlobalDomain;
using InfinityWorldChess.MainMenuDomain;
using InfinityWorldChess.RoleDomain;
using Secyud.Ugf;
using Secyud.Ugf.Collections;
using Secyud.Ugf.Modularity;

#endregion

[DependsOn(typeof(IwcMainPackageModule))]
public class StartupModule : IUgfModule, IOnPostConfigure
{
    public void Configure(ConfigurationContext context)
    {
    }

    public void PostConfigure(ConfigurationContext context)
    {
        // 资源预载 现在需要预载的资源很少，放在这里了，如果多的话应该开一个模块或者服务专门去做。
        RoleResourceManager roleResourceManager = context.Get<RoleResourceManager>();

        foreach (RegistrableDictionary<int, AvatarSpriteContainer> d in roleResourceManager.FemaleAvatarResource)
        {
            foreach (AvatarSpriteContainer container in d.ValueList)
            {
                _ = container.Sprite;
            }
        }

        foreach (RegistrableDictionary<int, AvatarSpriteContainer> d in roleResourceManager.MaleAvatarResource)
        {
            foreach (AvatarSpriteContainer container in d.ValueList)
            {
                _ = container.Sprite;
            }
        }
            

        // 打开游戏需要启用的域，域其实是管理资源用的。
        U.M.CreateScope<GlobalScope>();
        // 游戏开始需要打开主菜单
        U.M.CreateScope<MainMenuScope>();
        // 提供一个可以打开存档面板的功能，其实可以并入GlobalScope 或者 MainMenuScope
        U.M.CreateScope<ArchivingScope>();
    }
}