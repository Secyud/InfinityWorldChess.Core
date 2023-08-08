#region

using Secyud.Ugf.Collections;

#endregion

namespace InfinityWorldChess.BiographyDomain
{
	public class IwcBiographyGlobalService :IBiographyGlobalService
	{
		public  RegistrableList<IBiography> Biographies { get; } = new();
	}
}