using System.Collections.Generic;
using InfinityWorldChess.BiographyDomain;
using InfinityWorldChess.BundleDomain;
using InfinityWorldChess.PlayerDomain;
using InfinityWorldChess.RoleDomain;
using Secyud.Ugf.DependencyInjection;

namespace InfinityWorldChess.GameCreatorDomain
{
    [Registry(DependScope = typeof(GameCreatorScope))]
    public class GameCreatorContext : IRegistry
    {
        public Role Role { get; } = new()
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

        public List<IBundle> Bundles { get; } = new();
        public List<IBiography> Biography { get; } = new();
        public PlayerSetting PlayerSetting { get; } = new();
        public WorldMessageSetting WorldMessageSetting { get; } = new();
    }
}