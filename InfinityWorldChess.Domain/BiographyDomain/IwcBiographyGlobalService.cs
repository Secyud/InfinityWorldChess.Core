#region

using Secyud.Ugf.Collections;
using Secyud.Ugf.DependencyInjection;

#endregion

namespace InfinityWorldChess.BiographyDomain
{
	[Registry()]
	public class IwcBiographyGlobalService :IBiographyGlobalService
	{
		public  RegistrableList<IBiography> Biographies { get; } = new();
	}
}