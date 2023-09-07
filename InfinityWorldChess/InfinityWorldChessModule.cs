﻿#region

using System.Collections;
using InfinityWorldChess.GameCreatorDomain;
using InfinityWorldChess.GameDomain;
using InfinityWorldChess.GameDomain.WorldMapDomain;
using InfinityWorldChess.InteractionDomain;
using InfinityWorldChess.MapDomain;
using InfinityWorldChess.PlayerDomain;
using InfinityWorldChess.RoleDomain;
using Secyud.Ugf;
using Secyud.Ugf.Archiving;
using Secyud.Ugf.AssetComponents;
using Secyud.Ugf.AssetLoading;
using Secyud.Ugf.HexMap;
using Secyud.Ugf.Modularity;
using UnityEngine;

#endregion

namespace InfinityWorldChess
{
    [DependsOn(
        typeof(UgfCoreModule),
        typeof(UgfHexMapModule)
    )]
    public class InfinityWorldChessModule : IUgfModule, IPostConfigure,
        IOnPreInitialization, IOnInitialization, IOnPostInitialization, IOnArchiving
    {
        public const string AssetBundleName = "infinityworldchess";
        
        public void ConfigureGame(ConfigurationContext context)
        {
            context.Manager.AddAssembly(typeof(InfinityWorldChessModule).Assembly);
            context.AddResource<InfinityWorldChessResource>();
        }

        public void PostConfigureGame(ConfigurationContext context)
        {
            RegisterWorldModel(context.Get<WorldGlobalService>(), context.Get<IwcAb>());

            context.Get<WorldCellButtons>().Register(new TravelButtonDescriptor());
        }


        public IEnumerator OnGamePreInitialization(GameInitializeContext context)
        {
            IUgfApplication app = context.Get<IUgfApplication>();
            app.DependencyManager.CreateScope<GameScope>();
            app.DependencyManager.CreateScope<InteractionScope>();
            yield return null;
        }

        public IEnumerator OnGameInitializing(GameInitializeContext context)
        {
            if (SharedConsts.LoadGame)
            {
                yield return LoadGame();
            }
            else
            {
                yield return GameScope.Instance.World.OnGameCreation();
                yield return GameScope.Instance.Role.OnGameCreation();
                yield return GameScope.Instance.Player.OnGameCreation();
            }
        }

        public IEnumerator OnGamePostInitialization(GameInitializeContext context)
        {
            U.M.DestroyScope<GameCreatorScope>();
            SetPlayer(
                GameScope.Instance.World,
                GameScope.Instance.Player,
                GameScope.Instance.Role
            );
            yield return null;
        }

        public int GameInitializeStep { get; } = 30;


        public IEnumerator SaveGame()
        {
            yield return GameScope.Instance.World.OnGameSaving();
            yield return GameScope.Instance.Role.OnGameSaving();
            yield return GameScope.Instance.Player.OnGameSaving();
        }

        public IEnumerator LoadGame()
        {
            yield return GameScope.Instance.World.OnGameLoading();
            yield return GameScope.Instance.Role.OnGameLoading();
            yield return GameScope.Instance.Player.OnGameLoading();
        }


        public void OnGameShutdown()
        {
            U.M.DestroyScope<GameScope>();
        }

        private void SetPlayer(
            WorldGameContext world,
            PlayerGameContext player,
            RoleGameContext role)
        {
            Role pr = player.Role;
            HexUnit pu = world.WorldUnitPrefab.Instantiate(WorldGameContext.Map.Grid.transform);
            HexCell cell = pr.Position.Cell;
            WorldMap map = WorldGameContext.Map;
            role.Roles[pr.Id] = pr;
            player.Unit = pu;
            pu.Id = pr.Id;
            map.Grid.AddUnit(pr,pu, cell, 0);
            Vector3 position = pu.transform.position;
            position.y = 0;
            AvatarEditor avatar = pu.GetComponentInChildren<AvatarEditor>();
            avatar.OnInitialize(pr.Basic);
            map.MapCamera.transform.position = position;
        }

        private static void RegisterWorldModel(WorldGlobalService service, IAssetLoader ab)
        {
            PrefabContainer<Transform> GetPrefab(string name, string level, string type)
            {
                return PrefabContainer<Transform>.Create(ab, $"Features/{name}/{name} {level} {type}.prefab");
            }

            service.RegistrarResourceFeature(
                new FeatureDescriptor(0, 0,
                    GetPrefab("Ore", "Low", "1")),
                new FeatureDescriptor(0, 0, GetPrefab("Ore", "Low", "2")),
                new FeatureDescriptor(0, 1, GetPrefab("Ore", "Medium", "1")),
                new FeatureDescriptor(0, 1, GetPrefab("Ore", "Medium", "2")),
                new FeatureDescriptor(0, 2, GetPrefab("Ore", "High", "1")),
                new FeatureDescriptor(0, 2, GetPrefab("Ore", "High", "2")),
                new FeatureDescriptor(1, 0, GetPrefab("Plant", "Low", "1")),
                new FeatureDescriptor(1, 0, GetPrefab("Plant", "Low", "2")),
                new FeatureDescriptor(1, 1, GetPrefab("Plant", "Medium", "1")),
                new FeatureDescriptor(1, 1, GetPrefab("Plant", "Medium", "2")),
                new FeatureDescriptor(1, 2, GetPrefab("Plant", "High", "1")),
                new FeatureDescriptor(1, 2, GetPrefab("Plant", "High", "2")),
                new FeatureDescriptor(2, 0, GetPrefab("Farm", "Low", "1")),
                new FeatureDescriptor(2, 0, GetPrefab("Farm", "Low", "2")),
                new FeatureDescriptor(2, 1, GetPrefab("Farm", "Medium", "1")),
                new FeatureDescriptor(2, 1, GetPrefab("Farm", "Medium", "2")),
                new FeatureDescriptor(2, 2, GetPrefab("Farm", "High", "1")),
                new FeatureDescriptor(2, 2, GetPrefab("Farm", "High", "2"))
            );

            service.RegistrarSpecialFeature(0,
                PrefabContainer<Transform>.Create(ab, "Features/Special/Castle.prefab"));
            service.RegistrarSpecialFeature(1,
                PrefabContainer<Transform>.Create(ab, "Features/Special/Village.prefab"));
        }
    }
}