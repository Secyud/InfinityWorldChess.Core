#region

using InfinityWorldChess.ArchivingDomain;
using InfinityWorldChess.GlobalDomain;
using Secyud.Ugf.DependencyInjection;
using Secyud.Ugf.Modularity;

#endregion

namespace InfinityWorldChess
{
	[DependsOn(typeof(InfinityWorldChessModule))]
	public class StartupModule : IUgfModule,IOnPostConfigure
	{
		public void Configure(ConfigurationContext context)
		{
		}

		public void PostConfigure(ConfigurationContext context)
		{
			IDependencyManager manager = context.Get<IDependencyManager>();
			manager.CreateScope<GlobalScope>();
			manager.CreateScope<ArchivingScope>();
		}
	}
}