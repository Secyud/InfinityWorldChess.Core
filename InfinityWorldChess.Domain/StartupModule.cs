#region

using Secyud.Ugf.Modularity;

#endregion

namespace InfinityWorldChess
{
	[DependsOn(typeof(InfinityWorldChessDomainModule))]
	public class StartupModule : IUgfModule
	{
		public void ConfigureGame(ConfigurationContext context)
		{
		}
	}
}