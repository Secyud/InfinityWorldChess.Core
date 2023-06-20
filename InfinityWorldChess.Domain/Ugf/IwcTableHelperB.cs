#region

using Secyud.Ugf;
using Secyud.Ugf.ButtonComponents;
using Secyud.Ugf.TableComponents;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

#endregion

namespace InfinityWorldChess.Ugf
{
	public class IwcTableHelperB<TItem, TListService, TButtonService> : IwcTableHelperF<TItem, TListService>
		where TItem : ICanBeShown
		where TListService : TableFunctionBase<TItem>
		where TButtonService : ButtonFunctionBase<TItem>
	{
		private readonly IEnumerable<ButtonRegistration<TItem>> _buttons;
		private readonly List<ButtonRegistration<TItem>> _visibleButtons = new();

		private readonly UnityAction _refreshAction;
		protected IwcTableHelperB(UnityAction refreshAction)
		{
			_refreshAction = refreshAction;
			BindPrepareCellAction(PrepareCell);
			_buttons = Og.DefaultProvider.Get<TButtonService>().Get;
		}


		private void PrepareCell(NormalCell cell, int index)
		{
			cell.gameObject.GetOrAddButton(() => OnClick(index));
		}

		public override void OnInitialize(FunctionalTable table, NormalCell cellTemplate, IList<TItem> showItems)
		{
			_visibleButtons.Clear();
			_visibleButtons.AddRange(_buttons.Where(u => u.Visible()));
			base.OnInitialize(table, cellTemplate, showItems);
		}

		private void OnClick(int index)
		{
			IwcAb.Instance.ButtonGroupInk.Value.Create(this[index], _visibleButtons, _refreshAction);
		}
	}
}