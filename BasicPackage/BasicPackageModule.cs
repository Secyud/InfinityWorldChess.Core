#region

using System.Collections;
using InfinityWorldChess.ItemDomain;
using InfinityWorldChess.ItemDomain.BookDomain;
using InfinityWorldChess.ItemDomain.EquipmentDomain;
using InfinityWorldChess.ItemDomain.FoodDomain;
using InfinityWorldChess.RoleDomain;
using Secyud.Ugf.DependencyInjection;
using Secyud.Ugf.Modularity;
using System.IO;
using UnityEngine;

#endregion

namespace InfinityWorldChess
{
    [DependsOn(
        typeof(InfinityWorldChessModule)
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
            // context.Get<InteractionGlobalService>().FreeInteractions.RegisterList(
            //     new ChatInteraction(),
            //     new FightInteraction()
            // );
            RegisterItem(context);
            RegisterAvatar(context);

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