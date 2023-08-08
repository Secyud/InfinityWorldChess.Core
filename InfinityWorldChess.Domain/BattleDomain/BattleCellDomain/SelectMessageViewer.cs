#region

using System;
using Secyud.Ugf.BasicComponents;
using UnityEngine;

#endregion

namespace InfinityWorldChess.BattleDomain.BattleCellDomain
{
	public class SelectMessageViewer : MonoBehaviour
	{
		[SerializeField] private SText Position;

		private SelectRefreshItem _refreshItem;
		private void Awake()
		{
			_refreshItem ??= new SelectRefreshItem(nameof(SelectMessageViewer),Refresh);
		}

		public void Refresh()
		{
			Position.Set(_refreshItem.Service.SelectedCell.Cell.Coordinates.ToString()??"");
		}
	}
}