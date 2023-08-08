#region

using Secyud.Ugf.Collections;

#endregion

namespace InfinityWorldChess.BundleDomain
{
	public class IwcBundleGlobalService : IBundleGlobalService
	{
		public RegistrableList<IBundle> Bundles { get; } = new();
	}
}