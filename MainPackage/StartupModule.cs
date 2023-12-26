#region

using InfinityWorldChess.ArchivingDomain;
using InfinityWorldChess.GlobalDomain;
using InfinityWorldChess.MainMenuDomain;
using InfinityWorldChess.RoleDomain;
using Secyud.Ugf;
using Secyud.Ugf.Collections;
using Secyud.Ugf.DependencyInjection;
using Secyud.Ugf.Modularity;

#endregion

namespace InfinityWorldChess
{
    [DependsOn(typeof(InfinityWorldChessModule))]
    public class StartupModule : IUgfModule, IOnPostConfigure
    {
        public void Configure(ConfigurationContext context)
        {
        }

        public void PostConfigure(ConfigurationContext context)
        {
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
            

            U.M.CreateScope<GlobalScope>();
            U.M.CreateScope<MainMenuScope>();
            U.M.CreateScope<ArchivingScope>();
        }
    }
}