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
		
		private SelectObservedService _selectObservedService;
		private void Awake()
		{
			_selectObservedService = BattleScope.Instance.Get<SelectObservedService>();
			_selectObservedService.AddObserverObject(nameof(SelectMessageViewer),Refresh,gameObject);
		}

		public void Refresh()
		{
			Position.Set(_selectObservedService.SelectedCell.Cell.Coordinates.ToString()??"");
		}
	}
}