#region

using Secyud.Ugf.Collections;
using Secyud.Ugf.DependencyInjection;

#endregion

namespace InfinityWorldChess.BiographyDomain
{
	public interface IBiographyGlobalService:IRegistry
	{
		RegistrableList<IBiography> Biographies { get; } 
	}
}