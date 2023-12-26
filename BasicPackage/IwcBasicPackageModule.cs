#region

using System.Collections;
using InfinityWorldChess.ItemDomain;
using InfinityWorldChess.ItemDomain.BookDomain;
using InfinityWorldChess.ItemDomain.EquipmentDomain;
using InfinityWorldChess.ItemDomain.FoodDomain;
using Secyud.Ugf.DependencyInjection;
using Secyud.Ugf.Modularity;
using InfinityWorldChess.ActivityFunctions;
using InfinityWorldChess.DialogueDomain;
using InfinityWorldChess.DialogueFunctions;
using InfinityWorldChess.GameDomain;
using InfinityWorldChess.InteractionDomain;
using InfinityWorldChess.LevelDomain;
using InfinityWorldChess.SkillDomain;
using Secyud.Ugf;
using Secyud.Ugf.Archiving;
using Secyud.Ugf.DataManager;

#endregion

namespace InfinityWorldChess
{
    [DependsOn(
        typeof(IwcMainPackageModule)
    )]
    public class IwcBasicPackageModule : IUgfModule, IOnInitialization, IOnPostConfigure,IOnPostInitialization
    {
        public void Configure(ConfigurationContext context)
        {
            context.Get<IDependencyRegistrar>().AddAssembly(typeof(IwcBasicPackageModule).Assembly);
            context.AddStringResource<BasicPackageResource>();
        }

        public void PostConfigure(ConfigurationContext context)
        {
            RegisterItemComponents(context);
            Registerer.RegisterDefault(context.Manager);
        }

        private static void RegisterItemComponents(ConfigurationContext context)
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

            ChatRegister chat = context.Get<ChatRegister>();
            chat.Register(new ActivityDialogueChat());
        }

        public IEnumerator OnGameInitializing(GameInitializeContext context)
        {
            //TODO
            //U.Get<ManufacturingGameContext>().OnGameLoading();
            //U.Get<ManufacturingGameContext>().OnGameSaving();
            //U.Get<ManufacturingGameContext>().OnGameCreation();

            var role = GameScope.Instance.Player.Role;
            var coreSkill = U.Tm.ReadObjectFromResource<CoreSkill>("基础招式_砸");
            role.CoreSkill.TryAddLearnedSkill(coreSkill);
            role.CoreSkill.Set(coreSkill, 0, 1);
            coreSkill = U.Tm.ReadObjectFromResource<CoreSkill>("基础招式_抓");
            role.CoreSkill.TryAddLearnedSkill(coreSkill);
            role.CoreSkill.Set(coreSkill, 0, 0);
            return null;
        }


        public IEnumerator OnGamePostInitialization(GameInitializeContext context)
        {
            var cell = GameScope.Instance.GetCellR(14, 10);

            cell.Buttons.Add(new TriggerBattleLevelButton());

            yield return null;
        }
    }
}