#region

using InfinityWorldChess.BuffDomain;
using Secyud.Ugf.HexMap;
using System.Collections.Generic;
using UnityEngine;

#endregion

namespace InfinityWorldChess.BattleDomain
{
    public sealed class BattleCell : CellBase
    {
        private bool _inRange;

        private bool _releasable;

        private bool _selected;

        public List<BattleRole> Roles { get; } = new();

        public Dictionary<int, IBuff<BattleCell>> CheckerBuffs { get; } = new();

        public bool Releasable
        {
            get => _releasable;
            set
            {
                _releasable = value;
                SetHighlight();
            }
        }

        public bool InRange
        {
            get => _inRange;
            set
            {
                _inRange = value;
                SetHighlight();
            }
        }

        public bool Selected
        {
            get => _selected;
            set
            {
                _selected = value;
                SetHighlight();
            }
        }

        public int SpecialIndex { get; set; } = -1;

        public int ResourceType { get; set; } = -1;

        public int ResourceLevel { get; set; } = -1;

        public override void SetHighlight()
        {
            if (_inRange)
                Cell.EnableHighlight(Color.red);
            else if (_selected)
                Cell.EnableHighlight(Color.green);
            else if (_releasable)
                Cell.EnableHighlight(Color.yellow);
            else
                Cell.DisableHighlight();
        }
    }
}