#region

using System;
using System.Collections.Generic;
using InfinityWorldChess.BuffDomain;
using Secyud.Ugf.UgfHexMap;
using UnityEngine;

#endregion

namespace InfinityWorldChess.BattleDomain.BattleCellDomain
{
    public sealed class BattleCell : UgfCell
    {
        [Flags]
        public enum State
        {
            Releasable = 0x08,
            InRange = 0x04,
            Selected = 0x02,
            Hovered = 0x01
        }

        private State _state;

        public List<BattleRole> Roles { get; } = new();

        public Dictionary<int, IBuff<BattleCell>> CheckerBuffs { get; } = new();

        internal bool Releasable
        {
            get => _state.HasFlag(State.Releasable);
            set => SetState(State.Releasable, value);
        }

        internal bool InRange
        {
            get => _state.HasFlag(State.InRange);
            set => SetState(State.InRange, value);
        }

        internal bool Selected
        {
            get => _state.HasFlag(State.Selected);
            set => SetState(State.Selected, value);
        }

        internal bool Hovered
        {
            get => _state.HasFlag(State.Hovered);
            set => SetState(State.Hovered, value);
        }

        public int SpecialIndex { get; set; } = -1;

        public int ResourceType { get; set; } = -1;

        public int ResourceLevel { get; set; } = -1;

        private void SetState(State state,bool value)
        {
            if (value)
            {
                _state |= state;
            }
            else
            {
                _state &= ~state;
            }

            SetHighlight();
        }
        public void SetHighlight()
        {
            if (_state == 0)
            {
                DisableHighlight();
                return;
            }

            EnableHighlight((int)_state switch
            {
                < 2      => Color.red,
                < 2 << 1 => Color.green,
                < 2 << 2 => Color.yellow,
                _        => Color.yellow
            });
        }
    }
}