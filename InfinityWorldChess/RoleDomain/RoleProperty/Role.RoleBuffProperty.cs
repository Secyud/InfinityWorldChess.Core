#region

using InfinityWorldChess.BattleDomain;
using InfinityWorldChess.BuffDomain;
using System.Collections.Generic;

#endregion

namespace InfinityWorldChess.RoleDomain
{
	public partial class Role
	{
		private RoleIdBuffProperty _idBuffs ;
		public RoleIdBuffProperty IdBuffs => _idBuffs ??= new RoleIdBuffProperty(this);

		public class RoleIdBuffProperty : IdBuffProperty<Role>
		{
			public List<IOnBattleRoleInitialize> BattleInitializes { get; } = new();

			public RoleIdBuffProperty(Role target) 
			{
				Target = target;
			}

			protected override Role Target { get; }
		}
	}
}