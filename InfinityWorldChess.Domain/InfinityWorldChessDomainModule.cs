#region

using InfinityWorldChess.GlobalDomain;
using InfinityWorldChess.PlayerDomain;
using InfinityWorldChess.RoleDomain;
using InfinityWorldChess.WorldDomain;
using Secyud.Ugf;
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
    public class InfinityWorldChessDomainModule : IUgfModule, IPostConfigure, IOnGameArchiving, IOnPreInitialization,
        IOnPostInitialization, IOnGameShutdown
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

            context.Get<IDependencyScopeFactory>().CreateScope<GlobalScope>();
        }

        public void OnGamePreInitialization()
        {
            Og.ScopeFactory.CreateScope<GameScope>();
        }
        public void OnGameLoading(LoadingContext context)
        {
            Og.Get<GameScope,WorldGameContext>().OnGameLoading(context);
            Og.Get<GameScope,RoleGameContext>().OnGameLoading(context);
            Og.Get<GameScope,PlayerGameContext>().OnGameLoading(context);
        }

        public void OnGameSaving(SavingContext context)
        {
            Og.Get<GameScope,WorldGameContext>().OnGameSaving(context);
            Og.Get<GameScope,RoleGameContext>().OnGameSaving(context);
            Og.Get<GameScope,PlayerGameContext>().OnGameSaving(context);
        }



        public void OnGameCreation()
        {
           Og.Get<GameScope,WorldGameContext>().OnGameCreation();
           Og.Get<GameScope,RoleGameContext>().OnGameCreation();
           Og.Get<GameScope,PlayerGameContext>().OnGameCreation();
        }

        public void OnGameShutdown()
        {
            Og.ScopeFactory.DestroyScope<GameScope>();
        }

        public void OnGamePostInitialization()
        {
            Og.ScopeFactory.DestroyScope<CreatorScope>();
            SetPlayer(
                GameScope.WorldGameContext,
                GameScope.PlayerGameContext,
                GameScope.RoleGameContext
            );
        }


        private void SetPlayer(WorldGameContext world, PlayerGameContext player, RoleGameContext role)
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