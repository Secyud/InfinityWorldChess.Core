using Secyud.Ugf;
using Secyud.Ugf.ButtonComponents;
using Secyud.Ugf.TableComponents;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace InfinityWorldChess.RoleDomain.TableComponents
{
	public class RoleButtonTableHelper<TButtonService>: RoleTableHelper 
		where TButtonService : ButtonFunctionBase<Role>
	{
		private readonly IEnumerable<ButtonRegistration<Role>> _buttons;
		private readonly List<ButtonRegistration<Role>> _visibleButtons = new();

		public RoleButtonTableHelper()
		{
			BindPrepareCellAction(PrepareCell);
			_buttons = Og.DefaultProvider.Get<TButtonService>().Get;
		}


		private void PrepareCell(RoleAvatarCell cell, int index)
		{
			cell.gameObject.GetOrAddButton(() => OnClick(index));
		}

		public override void OnInitialize(FunctionalTable table, RoleAvatarCell cellTemplate, IList<Role> showItems)
		{
			_visibleButtons.Clear();
			_visibleButtons.AddRange(_buttons.Where(u => u.Visible()));
			base.OnInitialize(table, cellTemplate, showItems);
		}

		private void OnClick(int index)
		{
			IwcAb.Instance.ButtonGroupInk.Value.Create(this[index], _visibleButtons, Table.RefreshPage);
		}
	}
}