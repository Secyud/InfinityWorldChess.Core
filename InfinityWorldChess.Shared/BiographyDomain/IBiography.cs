#region

using InfinityWorldChess.RoleDomain;
using Secyud.Ugf;

#endregion

namespace InfinityWorldChess.BiographyDomain
{
	public interface IBiography : IShowable, IHasContent
	{
		void OnGameCreation(Role role);
	}
}