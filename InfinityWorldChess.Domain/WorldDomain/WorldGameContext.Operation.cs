#region

using InfinityWorldChess.RoleDomain;
using Secyud.Ugf.HexMap;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

#endregion

namespace InfinityWorldChess.WorldDomain
{
	public partial class WorldGameContext
	{
		private WorldChecker _hoverChecker;
		private WorldChecker _selectedChecker;
		private List<HexCell> _path;

		public WorldChecker HoverChecker
		{
			get => _hoverChecker;
			set
			{
				if (_hoverChecker == value) return;

				if (_hoverChecker is not null)
					if (_hoverChecker == _selectedChecker)
						_hoverChecker.Cell.EnableHighlight(Color.green);
					else
						_hoverChecker.SetHighLight();
				_hoverChecker = value;
				if (_hoverChecker != _selectedChecker)
					_hoverChecker.Cell.EnableHighlight(Color.white);
			}
		}
		public WorldChecker SelectedChecker
		{
			get => _selectedChecker;
			set
			{
				if (_selectedChecker == value) return;

				_selectedChecker?.SetHighLight();
				_selectedChecker = value;
				_selectedChecker?.Cell.EnableHighlight(Color.green);
				Ui.ChangeSelectedChecker(_selectedChecker);
			}
		}

		public List<HexCell> Path
		{
			get => _path;
			set
			{
				if (!_path.IsNullOrEmpty())
					foreach (HexCell cell in _path)
						GetChecker(cell).PathState = 0;

				_path = value;

				if (!_path.IsNullOrEmpty())
				{
					GetChecker(_path[0]).PathState = 2;

					for (int i = 1; i < _path.Count - 1; i++)
						GetChecker(_path[i]).PathState = 1;

					GetChecker(_path.Last()).PathState = 3;
				}
			}
		}

		public void Travel(HexUnit unit,Role role)
		{
			unit.Travel(Path);
			HexCell last = Path.Last();
			WorldChecker checkerTarget = GetChecker(last);
			Ui.ChangePlayerChecker(checkerTarget);
			role.Position = checkerTarget;
			Map.Grid.ClearPath();
			Path = null;
		}
	}
}