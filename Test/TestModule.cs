#region

using System.Collections;
using InfinityWorldChess.GameDomain;
using InfinityWorldChess.GameDomain.WorldCellDomain;
using InfinityWorldChess.ManufacturingDomain;
using Secyud.Ugf.Modularity;

#endregion

namespace InfinityWorldChess
{
    [DependsOn(typeof(BasicPackageModule))]
    public class TestModule : IUgfModule, IOnPostInitialization, IOnPostConfigure
    {
        public void Configure(ConfigurationContext context)
        {
            context.Manager.AddAssembly(typeof(TestModule).Assembly);
        }

        public void PostConfigure(ConfigurationContext context)
        {
        }

        public IEnumerator OnGamePostInitialization(GameInitializeContext context)
        {
            WorldCell cell = GameScope.Instance.GetCellR(14, 10);
            if (cell is not null)
            {
                cell.Buttons.Add(new ManufacturingButtonDescriptor { Type = 0 });
                cell.Buttons.Add(new ManufacturingButtonDescriptor { Type = 1 });
                cell.Buttons.Add(new ManufacturingButtonDescriptor { Type = 2 });
            }
            yield return null;
        }
    }
}