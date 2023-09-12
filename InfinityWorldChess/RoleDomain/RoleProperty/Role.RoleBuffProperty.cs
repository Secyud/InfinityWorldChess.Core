#region

using InfinityWorldChess.BattleDomain;
using InfinityWorldChess.BuffDomain;
using System.Collections.Generic;

#endregion

namespace InfinityWorldChess.RoleDomain
{
	public partial class Role
	{
		private RoleBuffProperty _buffs ;
		public RoleBuffProperty Buffs => _buffs ??= new RoleBuffProperty(this);

		public class RoleBuffProperty : BuffProperty<Role>
		{
			public List<IOnBattleRoleInitialize> BattleInitializes { get; } = new();

			public RoleBuffProperty(Role target) 
			{
				Target = target;
			}

			protected override Role Target { get; }
		}
	}
}