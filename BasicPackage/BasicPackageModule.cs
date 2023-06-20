#region

using InfinityWorldChess.GlobalDomain;
using InfinityWorldChess.InteractionDomain;
using InfinityWorldChess.ItemDomain;
using InfinityWorldChess.ItemDomain.BookDomain;
using InfinityWorldChess.ItemDomain.EquipmentDomain;
using InfinityWorldChess.ItemDomain.FoodDomain;
using InfinityWorldChess.ManufacturingDomain;
using InfinityWorldChess.RoleDomain;
using Secyud.Ugf;
using Secyud.Ugf.DependencyInjection;
using Secyud.Ugf.Modularity;
using Secyud.Ugf.Resource;
using System.IO;
using InfinityWorldChess.BasicBundle.CoreSkills;
using InfinityWorldChess.BasicBundle.Interactions;
using InfinityWorldChess.PlayerDomain;

#endregion

namespace InfinityWorldChess
{
    [DependsOn(
        typeof(InfinityWorldChessDomainModule)
    )]
    public class BasicPackageModule : IUgfModule, IOnGameArchiving, IPostConfigure
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

            context.Get<IDependencyScopeFactory>().CreateScope<GlobalScope>();
        }

        private void RegisterInitialize(InitializeManager manager)
        {

            string prefix = Path.Combine(Og.AppPath,"Data","ResourceManager");
            
            manager.RegisterFromBinary(Path.Combine(prefix,"CoreSkillTemplate-BasicBundle.binary"),typeof(CoreSkillTemplate));
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
                Path.Combine(Og.AppPath, "Data", "Portrait", "portrait.binary"), "basic_portrait", ab
            );
        }

        private static void RegisterItem(ConfigurationContext context)
        {
            ItemTf tf = context.Get<ItemTf>();

            tf.RegisterFilterGroup(ItemFilterType.GetGroup());

            tf.RegisterSorter(new ItemSorterType());

            context.Get<ItemGameBf>().RegisterList(
                new ItemNormalButtonEating(),
                new EquipmentButtonRegistration(),
                new ItemNormalButtonReading()
            );
        }

        public void OnGameLoading(LoadingContext context)
        {
            Og.Get<GameScope,ManufacturingGameContext>().OnGameLoading(context);
        }

        public void OnGameSaving(SavingContext context)
        {
            Og.Get<GameScope,ManufacturingGameContext>().OnGameSaving(context);
        }

        public void OnGameCreation()
        {
            Og.Get<GameScope,ManufacturingGameContext>().OnGameCreation();
        }
    }
}