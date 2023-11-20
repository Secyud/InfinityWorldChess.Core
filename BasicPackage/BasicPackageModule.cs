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
using InfinityWorldChess.GameDomain;
using InfinityWorldChess.SkillDomain;
using Secyud.Ugf;
using UnityEngine;

#endregion

namespace InfinityWorldChess
{
    [DependsOn(
        typeof(InfinityWorldChessModule)
    )]
    public class BasicPackageModule : IUgfModule, IPostConfigure, IOnInitialization
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

            IwcAssets assets = context.Get<IwcAssets>();

            resource.RegisterAvatarResourceFromPath(
                Path.Combine(Application.dataPath, "Data", "Portrait", "portrait.binary"), "basic_portrait", assets
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

            if (!SharedConsts.LoadGame)
            {
                var role = GameScope.Instance.Player.Role;
                var coreSkill = U.Tm.ConstructFromResource<CoreSkill>("基础招式_砸");
                role.CoreSkill.TryAddLearnedSkill(coreSkill);
                role.CoreSkill.Set(coreSkill, 0, 1);
                coreSkill = U.Tm.ConstructFromResource<CoreSkill>("基础招式_抓");
                role.CoreSkill.TryAddLearnedSkill(coreSkill);
                role.CoreSkill.Set(coreSkill, 0, 0);
            }

            return null;
        }

        public int GameInitializeStep { get; }
    }
}