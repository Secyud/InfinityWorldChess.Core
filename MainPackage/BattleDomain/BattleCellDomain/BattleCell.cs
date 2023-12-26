#region

using System;
using System.Collections.Generic;
using InfinityWorldChess.BuffDomain;
using Secyud.Ugf;
using Secyud.Ugf.HexMapExtensions;
using Secyud.Ugf.UgfHexMap;
using UnityEngine;

#endregion

namespace InfinityWorldChess.BattleDomain
{
    public sealed class BattleCell : UgfCell
    {
        [Flags]
        public enum State
        {
            Releasable = 0x01,
            InRange = 0x02,
            Selected = 0x04,
            Hovered = 0x08
        }

        private State _state;


        public BattleCell()
        {
            Buffs = new BuffCollection<BattleCell, IBattleCellBuff>(this);
            Properties = new PropertyCollection<BattleCell, IBattleCellProperty>(this);
        }
        
        public List<BattleUnit> Roles { get; } = new();
        public BuffCollection<BattleCell, IBattleCellBuff> Buffs { get; }
        public PropertyCollection<BattleCell, IBattleCellProperty> Properties { get; } 

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

        private void SetState(State state, bool value)
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
            U.Log($"Index: {Index}, State: {_state}");

            if (_state == 0)
            {
                DisableHighlight();
                return;
            }

            EnableHighlight((int)_state switch
            {
                >= 8 => Color.cyan,
                >= 4 => Color.yellow,
                >= 2 => Color.green,
                _    => Color.red
            });
        }

        public override void CreateMap()
        {
            base.CreateMap();
            if (!this.IsValid())
            {
                EnableHighlight(Color.black);
            }
        }
    }
}