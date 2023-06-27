using System.Collections.Generic;
using InfinityWorldChess.BiographyDomain;
using InfinityWorldChess.BundleDomain;
using InfinityWorldChess.PlayerDomain;
using InfinityWorldChess.RoleDomain;
using InfinityWorldChess.WorldDomain;
using Secyud.Ugf.AssetLoading;
using Secyud.Ugf.DependencyInjection;

namespace InfinityWorldChess.GlobalDomain
{
    [Registry(DependScope=typeof(GlobalScope))]
    public class CreatorScope : DependencyScopeProvider
    {
        private static IMonoContainer<GameCreatorComponent> _gameCreator;

        public readonly Role.BasicProperty Basic= new()
        {
            Avatar =
            {
                BackItem = new RoleAvatar.AvatarElement(),
                BackHair = new RoleAvatar.AvatarElement(),
                Body = new RoleAvatar.AvatarElement(),
                Head = new RoleAvatar.AvatarElement(),
                HeadFeature = new RoleAvatar.AvatarElement4(),
                NoseMouth = new RoleAvatar.AvatarElement2X(),
                Eye = new RoleAvatar.AvatarElement4(),
                Brow = new RoleAvatar.AvatarElement4(),
                FrontHair = new RoleAvatar.AvatarElement()
            }
        };
        public readonly List<IBundle> Bundles = new();
        public readonly Role.ItemProperty Item = new();
        public readonly Role.NatureProperty Nature = new();
        public readonly PlayerSetting PlayerSetting = new();
        public readonly WorldSetting WorldSetting = new();
        public readonly List<IBiography> Biography = new();
        
        public static CreatorScope Instance { get; private set; }

        public CreatorScope( IwcAb ab) 
        {
            _gameCreator ??= MonoContainer<GameCreatorComponent>.Create(ab);

            _gameCreator.Create();
            Instance = this;
        }

        public override void Dispose()
        {
            _gameCreator.Destroy();
            Instance = null;
        }
    }
}