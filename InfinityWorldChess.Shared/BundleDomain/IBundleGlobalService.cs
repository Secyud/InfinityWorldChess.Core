using Secyud.Ugf.Collections;

namespace InfinityWorldChess.BundleDomain
{
	public interface IBundleGlobalService
	{
		RegistrableList<IBundle> Bundles { get; }
	}
}