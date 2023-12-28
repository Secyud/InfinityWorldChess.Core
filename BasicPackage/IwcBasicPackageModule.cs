#region

using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using InfinityWorldChess.ItemDomain;
using InfinityWorldChess.ItemDomain.BookDomain;
using InfinityWorldChess.ItemDomain.EquipmentDomain;
using InfinityWorldChess.ItemDomain.FoodDomain;
using Secyud.Ugf.DependencyInjection;
using Secyud.Ugf.Modularity;
using InfinityWorldChess.ActivityFunctions;
using InfinityWorldChess.BattleDomain;
using InfinityWorldChess.DialogueDomain;
using InfinityWorldChess.DialogueFunctions;
using InfinityWorldChess.GameDomain;
using InfinityWorldChess.GameDomain.WorldCellDomain;
using InfinityWorldChess.InteractionDomain;
using InfinityWorldChess.ItemTemplates;
using InfinityWorldChess.LevelDomain;
using InfinityWorldChess.ManufacturingDomain.Equipments;
using InfinityWorldChess.RoleDomain;
using InfinityWorldChess.SkillDomain;
using InfinityWorldChess.Ugf;
using Secyud.Ugf;
using Secyud.Ugf.Archiving;
using Secyud.Ugf.AssetLoading;
using Secyud.Ugf.DataManager;
using Secyud.Ugf.TableComponents.FilterComponents;
using Secyud.Ugf.VirtualPath;

#endregion

namespace InfinityWorldChess
{
    /// <summary>
    /// 这个包被当作插件加载，它在主体上进行实现，增加了一些基础性的功能和数据模板，以及锻造烹饪制药关卡系统。
    /// </summary>
    [DependsOn(
        typeof(IwcMainPackageModule)
    )]
    public class IwcBasicPackageModule : IUgfModule, IOnInitialization, IOnPostConfigure, IOnPostInitialization
    {
        public void Configure(ConfigurationContext context)
        {
            context.Get<IDependencyRegistrar>().AddAssembly(typeof(IwcBasicPackageModule).Assembly);
        }

        public void PostConfigure(ConfigurationContext context)
        {
            //  注册筛选器，通过物品的类型进行筛选
            context.Get<ItemFilters>().Register(
                new FilterTriggerDescriptor<IItem>
                {
                    Name = "物品类型",
                    Filters =
                    {
                        new ItemFilterForType<IEquipment>(),
                        new ItemFilterForType<IEdible>(),
                        new ItemFilterForType<ReadableItem>(),
                        new ItemFilterForType<EquipmentMaterial>(),
                        new ItemFilterForType<EquipmentBlueprint>(),
                    }
                });

            // 注册排序器，根据物品的类型进行排序
            context.Get<ItemSorters>().Register(
                new ItemSorterForType());

            // 注册物品按钮，在游戏菜单内右击物品，可以出现装备、食用，阅读。
            context.Get<PlayerItemButtons>().RegisterList(
                new ItemNormalButtonEating(),
                new EquipmentButtonDescriptor(),
                new ItemNormalButtonReading()
            );

            // 在互动菜单上注册两个按钮，聊天和切磋
            context.Get<InteractionButtons>().RegisterList(
                new ChatButtonDescriptor(),
                new LightBattleButton());

            // 在聊天中添加一个选项，任务。
            context.Get<ChatRegister>().Register(
                new ActivityDialogueChat());
            
            // 这个从虚拟路径中注册内容，包括一般资源，战斗关卡，人物捏脸素材目录。
            RegisterResourceFolder(context.Provider, "Data/Resources");
            RegisterLevelsFolder(context.Provider, "Data/Levels");
            RegisterAvatarFromFolder(context.Provider, "Data/Portrait");

            // 注册姓名资源，这决定了随机人物的姓名
            RoleResourceManager resource = context.Get<RoleResourceManager>();
            IVirtualPathManager vpManager = context.Get<IVirtualPathManager>();
            foreach (string path in vpManager.GetFilesSingly("Data/NameResources/LastNames.json"))
            {
                resource.LastNames.RegisterList(path.GetStringListFromPath());
            }
            foreach (string path in vpManager.GetFilesSingly("Data/NameResources/FirstNameFrontFemale.binary"))
            {
                resource.FirstNameFrontFemale.RegisterList(path.GetCharListFromPath());
            }
            foreach (string path in vpManager.GetFilesSingly("Data/NameResources/FirstNameFrontMale.binary"))
            {
                resource.FirstNameFrontMale.RegisterList(path.GetCharListFromPath());
            }
            foreach (string path in vpManager.GetFilesSingly("Data/NameResources/FirstNameBehindFemale.binary"))
            {
                resource.FirstNameBehindFemale.RegisterList(path.GetCharListFromPath());
            }
            foreach (string path in vpManager.GetFilesSingly("Data/NameResources/FirstNameBehindMale.binary"))
            {
                resource.FirstNameBehindMale.RegisterList(path.GetCharListFromPath());
            }
            foreach (string path in vpManager.GetFilesSingly("Data/NameResources/FirstNamesMale.binary"))
            {
                resource.FirstNamesMale.RegisterList(path.GetStringListFromPath());
            }
            foreach (string path in vpManager.GetFilesSingly("Data/NameResources/FirstNamesFemale.binary"))
            {
                resource.FirstNamesFemale.RegisterList(path.GetStringListFromPath());
            }
        }

        public IEnumerator OnGameInitializing(GameInitializeContext context)
        {
            // 为玩家添加两个基础招式，按道理应该放在默认剧本配置里。
            Role role = GameScope.Instance.Player.Role;
            CoreSkill coreSkill = U.Tm.ReadObjectFromResource<CoreSkill>("基础招式_砸");
            role.CoreSkill.TryAddLearnedSkill(coreSkill);
            role.CoreSkill.Set(coreSkill, 0, 1);
            coreSkill = U.Tm.ReadObjectFromResource<CoreSkill>("基础招式_抓");
            role.CoreSkill.TryAddLearnedSkill(coreSkill);
            role.CoreSkill.Set(coreSkill, 0, 0);
            return null;
        }


        public IEnumerator OnGamePostInitialization(GameInitializeContext context)
        {
            WorldCell cell = GameScope.Instance.GetCellR(14, 10);
            // 在出生位置放一个关卡功能。
            cell.Buttons.Add(new TriggerBattleLevelButton());

            yield return null;
        }
        
        
        public static void RegisterResourceFolder(IDependencyProvider manager, string path)
        {
            TypeManager tm = manager.Get<TypeManager>();
            IEnumerable<string> files = manager.Get<IVirtualPathManager>().GetFilesInFolder(path);
            foreach (string file in files)
            {
                if (file.EndsWith(".binary"))
                {
                    try
                    {
                        using FileStream stream = File.OpenRead(file);
                        tm.AddResourcesFromStream(stream);
                    }
                    catch (Exception e)
                    {
                        U.LogError($"File analyse failed: {file}");
                        U.LogError(e);
                    }
                }
            }
        }


        public static void RegisterLevelsFolder(IDependencyProvider manager, string path)
        {
            BattleLevelGlobalContext battleLevelGlobalContext = manager.Get<BattleLevelGlobalContext>();
            IEnumerable<string> files = manager.Get<IVirtualPathManager>().GetFilesInFolder(path);
            foreach (string file in files)
            {
                if (file.EndsWith(".binary"))
                {
                    try
                    {
                        using FileStream stream = File.OpenRead(file);
                        IBattleLevel[] list = stream.ReadResourceObjects<IBattleLevel>().ToArray();
                        battleLevelGlobalContext.LevelList.RegisterList(list);
                    }
                    catch (Exception e)
                    {
                        U.LogError($"File analyse failed: {file}");
                        U.LogError(e);
                    }
                }
            }
        }


        public static void RegisterAvatarFromFolder(IDependencyProvider manager, string path)
        {
            RoleResourceManager resource = manager.Get<RoleResourceManager>();
            TypeManager typeManager = manager.Get<TypeManager>();

            IEnumerable<string> files = manager.Get<IVirtualPathManager>().GetFilesInFolder(path);
            foreach (string file in files)
            {
                if (file.EndsWith(".binary"))
                {
                    try
                    {
                        using FileStream stream = File.OpenRead(file);
                        using DefaultArchiveReader reader = new(stream);
                        Guid id = reader.ReadGuid();
                        TypeDescriptor descriptor = typeManager[id];
                        if (descriptor is null)
                        {
                            continue;
                        }
                        IAssetLoader loader = manager.Get(descriptor.Type) as IAssetLoader;

                        resource.RegisterAvatarResourceFromPath(reader, loader);
                    }
                    catch (Exception e)
                    {
                        U.LogError($"File analyse failed: {file}");
                        U.LogError(e);
                    }
                }
            }
        }
    }
}