#region

using System.Collections;
using InfinityWorldChess.InteractionDomain;
using InfinityWorldChess.ItemDomain;
using InfinityWorldChess.ItemDomain.BookDomain;
using InfinityWorldChess.ItemDomain.EquipmentDomain;
using InfinityWorldChess.ItemDomain.FoodDomain;
using InfinityWorldChess.RoleDomain;
using Secyud.Ugf.DependencyInjection;
using Secyud.Ugf.Modularity;
using System.IO;
using InfinityWorldChess.BasicBundle.Interactions;
using Secyud.Ugf.DataManager;
using UnityEngine;

#endregion

namespace InfinityWorldChess
{
    [DependsOn(
        typeof(InfinityWorldChessDomainModule)
    )]
    public class BasicPackageModule : IUgfModule, IPostConfigure,IOnInitialization
    {
        public void ConfigureGame(ConfigurationContext context)
        {
            context.Get<IDependencyRegistrar>().AddAssembly(typeof(BasicPackageModule).Assembly);
            context.AddResource<BasicPackageResource>();
        }

        public void PostConfigureGame(ConfigurationContext context)
        {
            context.Get<IInteractionGlobalService>().FreeInteractions.RegisterList(
                new ChatInteraction(),
                new FightInteraction()
            );
            RegisterItem(context);
            RegisterAvatar(context);
            RegisterInitialize(context.Get<InitializeManager>());

        }

        private void RegisterInitialize(InitializeManager manager)
        {

            string prefix = Path.Combine(Application.dataPath,"Data","ResourceManager");
            
            //manager.RegisterFromBinary(Path.Combine(prefix,"CoreSkillTemplate-BasicBundle.binary"),typeof(CoreSkillTemplate));
            // List<string> list = im.GetResourceList(typeof(CoreSkillTemplate));
            // RoleResourceManager rm = context.Get<RoleResourceManager>();
            // rm.CoreSkills.AddRange(list);

            InteractionAction.Register(manager);
        }

        private void RegisterAvatar(ConfigurationContext context)
        {
            RoleResourceManager resource = context.Get<RoleResourceManager>();

            IwcAb ab = context.Get<IwcAb>();

            resource.RegisterAvatarResourceFromPath(
                Path.Combine(Application.dataPath, "Data", "Portrait", "portrait.binary"), "basic_portrait", ab
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
        }

        public IEnumerator OnGameInitializing(GameInitializeContext context)
        {
            //TODO
            //U.Get<ManufacturingGameContext>().OnGameLoading();
            //U.Get<ManufacturingGameContext>().OnGameSaving();
            //U.Get<ManufacturingGameContext>().OnGameCreation();

            return null;
        }

        public int GameInitializeStep { get; }
    }
}