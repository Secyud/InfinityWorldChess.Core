#region

using Secyud.Ugf.Collections;

#endregion

namespace InfinityWorldChess.BiographyDomain
{
	public interface IBiographyGlobalService
	{
		RegistrableList<IBiography> Biographies { get; } 
	}
}