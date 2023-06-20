#region

using System.Collections.Generic;

#endregion

namespace InfinityWorldChess.RoleDomain
{
	public interface IRoleGenerator
	{
		public IEnumerable<Role> GenerateRole();
	}
}