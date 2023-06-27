#region

using InfinityWorldChess.GlobalDomain;
using Secyud.Ugf.DependencyInjection;
using Secyud.Ugf.Modularity;

#endregion

namespace InfinityWorldChess
{
	[DependsOn(typeof(InfinityWorldChessDomainModule))]
	public class StartupModule : IUgfModule, IPostConfigure
	{
		public void PostConfigureGame(ConfigurationContext context)
		{
			IDependencyManager manager = context.Get<IDependencyManager>();
			manager.CreateScope<MainMenuScope>();
		}

		public void ConfigureGame(ConfigurationContext context)
		{
		}
	}
}