#region

using Secyud.Ugf.Collections;
using Secyud.Ugf.DependencyInjection;

#endregion

namespace InfinityWorldChess.BiographyDomain
{
	public class IwcBiographyGlobalService :IBiographyGlobalService, ISingleton
	{
		public  RegistrableList<IBiography> Biographies { get; } = new();
	}
}