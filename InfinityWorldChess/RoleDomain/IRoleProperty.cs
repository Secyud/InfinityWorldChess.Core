using System;
using InfinityWorldChess.FunctionDomain;
using Secyud.Ugf;

namespace InfinityWorldChess.RoleDomain
{
	public interface IRoleProperty:IInstallable<Role>,IOverlayable<Role>,IHasId<Type>
	{
	}
}