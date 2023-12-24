#region

using System.Collections;
using System.IO;
using InfinityWorldChess.GameCreatorDomain;
using InfinityWorldChess.GameDomain;
using InfinityWorldChess.GameDomain.WorldCellDomain;
using InfinityWorldChess.GameDomain.WorldMapDomain;
using InfinityWorldChess.InteractionDomain;
using InfinityWorldChess.MessageDomain;
using InfinityWorldChess.RoleDomain;
using InfinityWorldChess.SkillDomain;
using Secyud.Ugf;
using Secyud.Ugf.Archiving;
using Secyud.Ugf.HexMap;
using Secyud.Ugf.Modularity;
using Secyud.Ugf.VirtualPath;
using UnityEngine;

#endregion

namespace InfinityWorldChess
{
    [DependsOn(
        typeof(UgfCoreModule),
        typeof(UgfHexMapModule),
        typeof(UgfSteamModule)
    )]
    public class InfinityWorldChessModule : IUgfModule, IOnPostConfigure,
        IOnPreInitialization, IOnInitialization, IOnPostInitialization, IOnArchiving
    {
        public const string AssetBundleName = "infinityworldchess";

        public void Configure(ConfigurationContext context)
        {
            context.Manager.AddAssembly(typeof(InfinityWorldChessModule).Assembly);

            context.AddStringResource<InfinityWorldChessResource>();


            context.Get<WorldCellButtons>().Register(new TravelButtonDescriptor());

            IVirtualPathManager virtualPathManager = context.Get<IVirtualPathManager>();
            virtualPathManager.AddDirectory("Data", Path.Combine(U.Path, "Data"));
            virtualPathManager.AddDirectory("Localization", Path.Combine(U.Path, "Localization"));
        }

        public void PostConfigure(ConfigurationContext context)
        {
            U.Get<PassiveSkillButtons>()
                .Register(new PassiveSkillPointDivisionButton());
            U.Get<FormSkillButtons>()
                .Register(new FormSkillPointDivisionButton());
            U.Get<CoreSkillButtons>()
                .Register(new CoreSkillPointDivisionButton());
        }
        
        public IEnumerator OnGamePreInitialization(GameInitializeContext context)
        {
            U.M.CreateScope<GameScope>();
            yield return null;
        }

        public IEnumerator OnGameInitializing(GameInitializeContext context)
        {
            yield return GameScope.Instance.World.OnGameCreation();
            yield return GameScope.Instance.Role.OnGameCreation();
            yield return GameScope.Instance.Player.OnGameCreation();
        }
        public IEnumerator OnGamePostInitialization(GameInitializeContext context)
        {
            U.M.DestroyScope<GameCreatorScope>();

            WorldMap map = GameScope.Instance.Map;
            Role role = GameScope.Instance.Player.Role;
            HexUnit unit = GameScope.Instance.World
                .WorldUnitPrefab.Instantiate(map.transform);
            unit.Initialize(role.Id, map, role.Position);
            GameScope.Instance.Player.Unit = unit;
            U.Get<CurrentTabService>().Cell = role.Position;
            unit.GetComponentInChildren<AvatarEditor>().OnInitialize(role.Basic);
            map.MapCamera.transform.position = unit.transform.position;


            yield return null;
        }

        public IEnumerator SaveGame()
        {
            {
                using FileStream stream = File.OpenWrite(IWCC.SaveFilePath("slot"));
                using DefaultArchiveWriter writer = new(stream);
                GameScope.Instance.Player.Role.Basic.Save(writer);
            }
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