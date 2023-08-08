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
        private static IMonoContainer<GameCreatorPanel> _gameCreator;

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

        public GameCreatorScope(IwcAb ab)
        {
            _gameCreator ??= MonoContainer<GameCreatorPanel>.Create(ab);

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