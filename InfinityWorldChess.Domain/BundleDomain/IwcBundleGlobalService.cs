#region

using Secyud.Ugf.Collections;
using Secyud.Ugf.DependencyInjection;

#endregion

namespace InfinityWorldChess.BundleDomain
{
	[Registry()]
	public class IwcBundleGlobalService : IBundleGlobalService
	{
		public RegistrableList<IBundle> Bundles { get; } = new();
	}
}