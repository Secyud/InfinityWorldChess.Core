#region

using Secyud.Ugf;
using Secyud.Ugf.Modularity;

#endregion

namespace InfinityWorldChess
{
	[DependsOn(
		typeof(UgfCoreModule),
		typeof(UgfHexMapModule)
	)]
	public class InfinityWorldChessSharedModule : IUgfModule, IPostConfigure
	{
		public const string AssetBundleName = "infinityworldchess";

		public void ConfigureGame(ConfigurationContext context)
		{
		}

		public void PostConfigureGame(ConfigurationContext context)
		{
		}
	}
}