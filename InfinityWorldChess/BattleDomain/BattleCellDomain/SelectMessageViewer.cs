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
		
		private void Awake()
		{
			BattleScope.Instance.Context.SelectedCellService
				.AddObserverObject(nameof(SelectMessageViewer),Refresh,gameObject);
		}

		public void Refresh()
		{
			Position.Set(
				BattleScope.Instance.Context.SelectedCell.Coordinates.ToString()??"");
		}
	}
}