using System.Collections.Generic;
using InfinityWorldChess.BiographyDomain;
using InfinityWorldChess.BundleDomain;
using InfinityWorldChess.GlobalDomain;
using InfinityWorldChess.PlayerDomain;
using InfinityWorldChess.RoleDomain;
using Secyud.Ugf.AssetComponents;
using Secyud.Ugf.DependencyInjection;

namespace InfinityWorldChess.GameCreatorDomain
{
    [Registry(DependScope = typeof(GlobalScope))]
    public class GameCreatorScope : DependencyScopeProvider
    {
        private readonly IMonoContainer<GameCreatorPanel> _gameCreator;

        public readonly Role Role = new()
        {
            BodyPart =
            {
                Living =
                {
                    MaxValue = 10,
                    RealValue = 10
                },
                Kiling =
                {
                    MaxValue = 10,
                    RealValue = 10
                },
                Nimble =
                {
                    MaxValue = 10,
                    RealValue = 10
                },
                Defend =
                {
                    MaxValue = 10,
                    RealValue = 10
                }
            }
        };

        public readonly List<IBundle> Bundles = new();
        public readonly List<IBiography> Biography = new();
        public readonly PlayerSetting PlayerSetting = new();
        public readonly WorldSetting WorldSetting = new();

        public static GameCreatorScope Instance { get; private set; }

       
        
        public GameCreatorScope(IwcAssets assets)
        {
            _gameCreator = MonoContainer<GameCreatorPanel>.Create(assets);
        }

        public override void OnInitialize()
        {
            Instance = this;
            _gameCreator.Create();
        }

        public override void Dispose()
        {
            _gameCreator.Destroy();
            Instance = null;
        }
    }
}