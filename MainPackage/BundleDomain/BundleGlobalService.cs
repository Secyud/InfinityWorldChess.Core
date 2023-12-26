#region

using Secyud.Ugf.Collections;
using Secyud.Ugf.DependencyInjection;

#endregion

namespace InfinityWorldChess.BundleDomain
{
	public class BundleGlobalService : IRegistry
	{
		public RegistrableList<IBundle> Bundles { get; } = new();
	}
}