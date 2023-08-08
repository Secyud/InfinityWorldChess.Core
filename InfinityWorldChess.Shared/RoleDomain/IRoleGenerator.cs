#region

using System.Collections.Generic;
using Secyud.Ugf.DependencyInjection;

#endregion

namespace InfinityWorldChess.RoleDomain
{
	public interface IRoleGenerator:IRegistry
	{
		public IEnumerable<Role> GenerateRole();
	}
}