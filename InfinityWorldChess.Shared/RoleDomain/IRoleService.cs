#region

using Secyud.Ugf.DependencyInjection;
using UnityEngine;

#endregion

namespace InfinityWorldChess.RoleDomain
{
	public interface IRoleService:IRegistry
	{
		void SetSmallContent(Transform cell,Role role);

		void SetContent(Transform transform, Role role);
	}
}