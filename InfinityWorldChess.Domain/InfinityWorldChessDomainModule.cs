#region

using System.Collections;
using InfinityWorldChess.ArchivingDomain;
using InfinityWorldChess.GlobalDomain;
using InfinityWorldChess.PlayerDomain;
using InfinityWorldChess.RoleDomain;
using InfinityWorldChess.WorldDomain;
using Secyud.Ugf;
using Secyud.Ugf.Archiving;
using Secyud.Ugf.AssetLoading;
using Secyud.Ugf.DependencyInjection;
using Secyud.Ugf.HexMap;
using Secyud.Ugf.Modularity;
using UnityEngine;

#endregion

namespace InfinityWorldChess
{
    [DependsOn(
        typeof(InfinityWorldChessSharedModule),
        typeof(UgfHexMapModule)
    )]
    public class InfinityWorldChessDomainModule : IUgfModule, IPostConfigure,
        IOnPreInitialization, IOnInitialization, IOnPostInitialization, IOnArchiving
    {
        public const string AssetBundleName = InfinityWorldChessSharedModule.AssetBundleName;

        public void ConfigureGame(ConfigurationContext context)
        {
            context.Manager.AddAssembly(typeof(InfinityWorldChessDomainModule).Assembly);
            context.AddResource<InfinityWorldChessResource>();
        }

        public void PostConfigureGame(ConfigurationContext context)
        {
            RegisterWorldModel(context.Get<WorldGlobalService>(), context.Get<IwcAb>());

            context.Get<WorldHexCellBf>().Register(new TravelButtonRegistration());

            IDependencyManager manager = context.Get<IDependencyManager>();
            manager.CreateScope<GlobalScope>();
        }


        public IEnumerator OnGamePreInitialization(GameInitializeContext context)
        {
            IUgfApplication app = context.Get<IUgfApplication>();
            app.DependencyManager.CreateScope<GameScope>();
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
            U.Factory.Application.DependencyManager.DestroyScope<CreatorScope>();
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
            U.Factory.Application.DependencyManager.DestroyScope<GameScope>();
        }

        private void SetPlayer(
            WorldGameContext world,
            PlayerGameContext player,
            RoleGameContext role)
        {
            Role pr = player.Role;
            HexUnit pu = world.WorldUnitPrefab.Instantiate(world.Map.Grid.transform);
            HexCell cell = pr.Position.Cell;
            WorldMapComponent map = world.Map;
            role.Roles[pr.Id] = pr;
            player.Unit = pu;
            pu.Id = pr.Id;
            map.Grid.AddUnit(pu, cell, 0);
            Vector3 position = pu.transform.position;
            position.y = 0;
            map.MapCamera.transform.position = position;
            world.Ui.SelectLeftPanel(0);
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