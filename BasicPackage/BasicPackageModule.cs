#region

using System.Collections;
using System.Collections.Generic;
using InfinityWorldChess.ItemDomain;
using InfinityWorldChess.ItemDomain.BookDomain;
using InfinityWorldChess.ItemDomain.EquipmentDomain;
using InfinityWorldChess.ItemDomain.FoodDomain;
using InfinityWorldChess.RoleDomain;
using Secyud.Ugf.DependencyInjection;
using Secyud.Ugf.Modularity;
using System.IO;
using InfinityWorldChess.ActivityFunctions;
using InfinityWorldChess.DialogueDomain;
using InfinityWorldChess.DialogueFunctions;
using InfinityWorldChess.GameDomain;
using InfinityWorldChess.InteractionDomain;
using InfinityWorldChess.LevelDomain;
using InfinityWorldChess.SkillDomain;
using Secyud.Ugf;
using Secyud.Ugf.DataManager;
using UnityEngine;

#endregion

namespace InfinityWorldChess
{
    [DependsOn(
        typeof(InfinityWorldChessModule)
    )]
    public class BasicPackageModule : IUgfModule, IOnInitialization,IOnPostConfigure
    {
        public void Configure(ConfigurationContext context)
        {
            context.Get<IDependencyRegistrar>().AddAssembly(typeof(BasicPackageModule).Assembly);
            context.AddStringResource<BasicPackageResource>();
        }
        
        public void PostConfigure(ConfigurationContext context)
        {
            RegisterItem(context);
            RegisterAvatar(context);
            
            TypeManager tm = context.Get<TypeManager>();
            
            string path = Path.Combine(U.Path, "Data/Resource/basic-bundle.binary");
            using FileStream file = File.OpenRead(path);
            tm.AddResourcesFromStream(file);

            
            BattleLevelGlobalContext battleLevelGlobalContext = context.Get<BattleLevelGlobalContext>();
            battleLevelGlobalContext.LevelList.RegisterList();

            using FileStream stream = File.OpenRead(Path.Combine(U.Path, "Data/Resource/levels.binary"));
            IBattleLevel[] list = stream.ReadResourceObjects<IBattleLevel>().ToArray();
            battleLevelGlobalContext.LevelList.RegisterList(list);
        }

        private void RegisterAvatar(ConfigurationContext context)
        {
            RoleResourceManager resource = context.Get<RoleResourceManager>();

            IwcAssets assets = context.Get<IwcAssets>();

            resource.RegisterAvatarResourceFromPath(
                Path.Combine(Application.dataPath, "Data", "Portrait", "portrait.binary"), assets
            );
        }

        private static void RegisterItem(ConfigurationContext context)
        {
            ItemFilters filters = context.Get<ItemFilters>();

            filters.RegisterList(ItemFilterToggleType.GetGroup());

            ItemSorters sorters = context.Get<ItemSorters>();

            sorters.RegisterList(new ItemSorterType());

            context.Get<PlayerItemButtons>().RegisterList(
                new ItemNormalButtonEating(),
                new EquipmentButtonDescriptor(),
                new ItemNormalButtonReading()
            );
            
            WorldCellRoleDefaultButtons.RegistrarButtons(context.Get<InteractionButtons>());
            
            ChatRegister chat = context.Get<ChatRegister>() ;
            chat.Register(new ActivityDialogueChat());
        }

        public IEnumerator OnGameInitializing(GameInitializeContext context)
        {
            //TODO
            //U.Get<ManufacturingGameContext>().OnGameLoading();
            //U.Get<ManufacturingGameContext>().OnGameSaving();
            //U.Get<ManufacturingGameContext>().OnGameCreation();

            if (!IWCC.LoadGame)
            {
                var role = GameScope.Instance.Player.Role;
                var coreSkill = U.Tm.ReadObjectFromResource<CoreSkill>("基础招式_砸");
                role.CoreSkill.TryAddLearnedSkill(coreSkill);
                role.CoreSkill.Set(coreSkill, 0, 1);
                coreSkill = U.Tm.ReadObjectFromResource<CoreSkill>("基础招式_抓");
                role.CoreSkill.TryAddLearnedSkill(coreSkill);
                role.CoreSkill.Set(coreSkill, 0, 0);
            }

            var cell = GameScope.Instance.GetCellR(14,10);

            cell.Buttons.Add(new TriggerBattleLevelButton());

            return null;
        }

        public int GameInitializeStep => 0x1000;
    }
}