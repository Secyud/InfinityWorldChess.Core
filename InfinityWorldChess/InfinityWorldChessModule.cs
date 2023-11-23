#region

using System.Collections;
using System.IO;
using InfinityWorldChess.ActivityDomain;
using InfinityWorldChess.DialogueDomain;
using InfinityWorldChess.GameCreatorDomain;
using InfinityWorldChess.GameDomain;
using InfinityWorldChess.GameDomain.WorldCellDomain;
using InfinityWorldChess.GameDomain.WorldMapDomain;
using InfinityWorldChess.InteractionDomain;
using InfinityWorldChess.MessageDomain;
using InfinityWorldChess.PlayerDomain;
using InfinityWorldChess.RoleDomain;
using Secyud.Ugf;
using Secyud.Ugf.Archiving;
using Secyud.Ugf.DataManager;
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
    public class InfinityWorldChessModule : IUgfModule, IOnPostConfigure,
        IOnPreInitialization, IOnInitialization, IOnPostInitialization, IOnArchiving
    {
        public const string AssetBundleName = "infinityworldchess";
        
        public void Configure(ConfigurationContext context)
        {
            context.Manager.AddAssembly(typeof(InfinityWorldChessModule).Assembly);
            context.AddResource<InfinityWorldChessResource>();
        }

        public void PostConfigure(ConfigurationContext context)
        {
            //RegisterWorldModel(context.Get<WorldGlobalService>(), context.Get<IwcAssets>());
            WorldCellRoleDefaultButtons.RegistrarButtons(context.Get<InteractionButtons>());
            context.Get<WorldCellButtons>().Register(new TravelButtonDescriptor());

            TypeManager tm = context.Get<TypeManager>();
            string path = Path.Combine(U.Path, "Data/Resource/te.binary");

            using FileStream file = File.OpenRead(path);
            tm.LoadResourcesFromStream(file);
            
            
            ChatRegister chat = context.Get<ChatRegister>() ;
            chat.Register(new ActivityListDialogue());
        }


        public IEnumerator OnGamePreInitialization(GameInitializeContext context)
        {
            IUgfApplication app = context.Get<IUgfApplication>();
            app.DependencyManager.CreateScope<GameScope>();
            app.DependencyManager.CreateScope<InteractionScope>();
            app.DependencyManager.CreateScope<MessageScope>();
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


        public void OnGameShutdown()
        {
            U.M.DestroyScope<GameScope>();
            U.M.DestroyScope<InteractionScope>();
            U.M.DestroyScope<MessageScope>();
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

        private void SetPlayer(
            WorldGameContext world,
            PlayerGameContext player,
            RoleGameContext role)
        {
            Role pr = player.Role;
            HexUnit pu = world.WorldUnitPrefab.Instantiate(WorldGameContext.Map.transform);
            HexCell cell = pr.Position;
            WorldMap map = WorldGameContext.Map;
            player.Unit = pu;
            pu.Id = pr.Id;
            GameScope.Instance.Get<CurrentTabService>().Cell = (WorldCell)cell; 
            map.AddUnit(pu, cell, 0);
            Vector3 position = pu.transform.position;
            position.y = 0;
            AvatarEditor avatar = pu.GetComponentInChildren<AvatarEditor>();
            avatar.OnInitialize(pr.Basic);
            map.MapCamera.transform.position = position;
        }
        //
        // private static void RegisterWorldModel(WorldGlobalService service, IAssetLoader ab)
        // {
        //     PrefabContainer<Transform> GetPrefab(string name, string level, string type)
        //     {
        //         return PrefabContainer<Transform>.Create(ab, $"Features/{name}/{name} {level} {type}.prefab");
        //     }
        //
        //     service.RegistrarResourceFeature(
        //         new FeatureDescriptor(0, 0, GetPrefab("Ore", "Low", "1")),
        //         new FeatureDescriptor(0, 0, GetPrefab("Ore", "Low", "2")),
        //         new FeatureDescriptor(0, 1, GetPrefab("Ore", "Medium", "1")),
        //         new FeatureDescriptor(0, 1, GetPrefab("Ore", "Medium", "2")),
        //         new FeatureDescriptor(0, 2, GetPrefab("Ore", "High", "1")),
        //         new FeatureDescriptor(0, 2, GetPrefab("Ore", "High", "2")),
        //         new FeatureDescriptor(1, 0, GetPrefab("Plant", "Low", "1")),
        //         new FeatureDescriptor(1, 0, GetPrefab("Plant", "Low", "2")),
        //         new FeatureDescriptor(1, 1, GetPrefab("Plant", "Medium", "1")),
        //         new FeatureDescriptor(1, 1, GetPrefab("Plant", "Medium", "2")),
        //         new FeatureDescriptor(1, 2, GetPrefab("Plant", "High", "1")),
        //         new FeatureDescriptor(1, 2, GetPrefab("Plant", "High", "2")),
        //         new FeatureDescriptor(2, 0, GetPrefab("Farm", "Low", "1")),
        //         new FeatureDescriptor(2, 0, GetPrefab("Farm", "Low", "2")),
        //         new FeatureDescriptor(2, 1, GetPrefab("Farm", "Medium", "1")),
        //         new FeatureDescriptor(2, 1, GetPrefab("Farm", "Medium", "2")),
        //         new FeatureDescriptor(2, 2, GetPrefab("Farm", "High", "1")),
        //         new FeatureDescriptor(2, 2, GetPrefab("Farm", "High", "2"))
        //     );
        //
        //     service.RegistrarSpecialFeature(0,
        //         PrefabContainer<Transform>.Create(ab, "Features/Special/Castle.prefab"));
        //     service.RegistrarSpecialFeature(1,
        //         PrefabContainer<Transform>.Create(ab, "Features/Special/Village.prefab"));
        // }
    }
}