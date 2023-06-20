#region

using UnityEngine;

#endregion

namespace InfinityWorldChess.RoleDomain
{
	public interface IRoleService
	{
		void SetSmallContent(Transform cell,Role role);

		void SetContent(Transform transform, Role role);
	}
}