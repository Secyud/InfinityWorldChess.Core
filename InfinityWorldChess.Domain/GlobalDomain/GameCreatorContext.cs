#region

using InfinityWorldChess.BiographyDomain;
using InfinityWorldChess.BundleDomain;
using InfinityWorldChess.PlayerDomain;
using InfinityWorldChess.RoleDomain;
using InfinityWorldChess.WorldDomain;
using Secyud.Ugf.DependencyInjection;
using System.Collections.Generic;

#endregion

namespace InfinityWorldChess.GlobalDomain
{
    public class GameCreatorContext : IScoped
    {
        public readonly Role.BasicProperty Basic;
        public readonly List<IBundle> Bundles = new();
        public readonly Role.ItemProperty Item = new();
        public readonly Role.NatureProperty Nature = new();
        public readonly PlayerSetting PlayerSetting = new();
        public readonly WorldSetting WorldSetting = new();
        public readonly List<IBiography> Biography = new();

        public GameCreatorContext()
        {
            Basic = new Role.BasicProperty()
            {
                Avatar =
                {
                    BackItem = new RoleAvatar.AvatarElement(0),
                    BackHair = new RoleAvatar.AvatarElement(0),
                    Body = new RoleAvatar.AvatarElement(0),
                    Head = new RoleAvatar.AvatarElement(0),
                    HeadFeature = new RoleAvatar.AvatarElement4(0),
                    NoseMouth = new RoleAvatar.AvatarElement2X(0, 0),
                    Eye = new RoleAvatar.AvatarElement4(0),
                    Brow = new RoleAvatar.AvatarElement4(0),
                    FrontHair = new RoleAvatar.AvatarElement(0)
                }
            };
        }
    }
}