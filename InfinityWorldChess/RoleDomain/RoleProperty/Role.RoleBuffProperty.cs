#region

using InfinityWorldChess.BattleDomain;
using InfinityWorldChess.BuffDomain;
using System.Collections.Generic;
using InfinityWorldChess.FunctionDomain;

#endregion

namespace InfinityWorldChess.RoleDomain
{
	public partial class Role
	{
		private RoleBuffProperty _buffs ;
		public RoleBuffProperty Buffs => _buffs ??= new RoleBuffProperty(this);

		public class RoleBuffProperty : BuffCollection<Role,IRoleBuff>
		{
			public List<IActionable<BattleUnit>> BattleInitializes { get; } = new();

			public RoleBuffProperty(Role target) : base(target)
			{
			}
		}
	}
}