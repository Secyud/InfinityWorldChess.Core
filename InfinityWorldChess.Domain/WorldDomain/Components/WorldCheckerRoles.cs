#region

using InfinityWorldChess.PlayerDomain;
using InfinityWorldChess.RoleDomain;
using InfinityWorldChess.RoleDomain.TableComponents;
using Secyud.Ugf.TableComponents;
using System.Linq;
using UnityEngine;

#endregion

namespace InfinityWorldChess.WorldDomain
{
	public class WorldCheckerRoles : WorldCheckerInitialize
	{
		[SerializeField] private FunctionalTable Table;

		private RoleButtonTableHelper<RoleWorldCheckerBf> _buttonTableHelperBh;
		private PlayerGameContext _playerGameContext;

		private void Awake()
		{
			_playerGameContext ??= GameScope.PlayerGameContext;
			_buttonTableHelperBh = new RoleButtonTableHelper<RoleWorldCheckerBf>();
		}

		public override void OnInitialize(WorldChecker checker)
		{
			_buttonTableHelperBh.OnInitialize(
				Table, 
				IwcAb.Instance.RoleAvatarCell.Value, 
				checker.InRoles
					.Where(u=>u!=_playerGameContext.Role).ToList());
		}
	}
}