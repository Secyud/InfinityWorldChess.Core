using System;
using InfinityWorldChess.FunctionDomain;
using Secyud.Ugf;

namespace InfinityWorldChess.RoleDomain
{
	public interface IRoleProperty:IEquippable<Role>,IOverlayable<Role>,IHasId<Type>
	{
	}
}