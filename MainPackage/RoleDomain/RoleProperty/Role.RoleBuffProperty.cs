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

		/// <summary>
		/// buff，以及添加了一个战斗初始化的列表，用于存放战斗中会初始化的内容，
		/// 如内功的效果，装备的效果。
		/// </summary>
		public class RoleBuffProperty : BuffCollection<Role,IRoleBuff>
		{
			public List<IActionable<BattleUnit>> BattleInitializes { get; } = new();

			public RoleBuffProperty(Role target) : base(target)
			{
			}
		}
	}
}