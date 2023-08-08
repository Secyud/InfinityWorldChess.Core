using Secyud.Ugf.Collections;
using Secyud.Ugf.DependencyInjection;

namespace InfinityWorldChess.BundleDomain
{
	public interface IBundleGlobalService:IRegistry
	{
		RegistrableList<IBundle> Bundles { get; }
	}
}