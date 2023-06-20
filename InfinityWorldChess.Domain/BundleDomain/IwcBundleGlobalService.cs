#region

using Secyud.Ugf.Collections;
using Secyud.Ugf.DependencyInjection;

#endregion

namespace InfinityWorldChess.BundleDomain
{
	public class IwcBundleGlobalService : IBundleGlobalService,ISingleton
	{
		public RegistrableList<IBundle> Bundles { get; } = new();
	}
}