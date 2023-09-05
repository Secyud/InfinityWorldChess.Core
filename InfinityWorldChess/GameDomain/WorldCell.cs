#region

using System;
using System.Collections.Generic;
using InfinityWorldChess.RoleDomain;
using Secyud.Ugf.Archiving;
using Secyud.Ugf.HexMap;
using Secyud.Ugf.TableComponents.ButtonComponents;

#endregion

namespace InfinityWorldChess.GameDomain
{
    public class WorldCell : CellBase, IArchivable
    {
        private int _specialIndex = -1;
        private int _stone;
        private int _tree;
        private int _farm;
        private byte _pathState;


        public byte PathState
        {
            get => _pathState;
            set
            {
                _pathState = value;

                SetHighlight();
            }
        }

        public override void SetHighlight()
        {
            switch (_pathState)
            {
                case 1:
                    Cell.EnableHighlight(HexGrid.PathCellColor);
                    break;
                case 2:
                    Cell.EnableHighlight(HexGrid.FromCellColor);
                    break;
                case 3:
                    Cell.EnableHighlight(HexGrid.ToCellColor);
                    break;
                default:
                    Cell.DisableHighlight();
                    break;
            }
        }

        public int Stone
        {
            get => _stone;
            set
            {
                _stone = value;
                Cell.RefreshSelfOnly();
            }
        }

        public int Tree
        {
            get => _tree;
            set
            {
                _tree = value;
                Cell.RefreshSelfOnly();
            }
        }

        public int Farm
        {
            get => _farm;
            set
            {
                _farm = value;
                Cell.RefreshSelfOnly();
            }
        }

        public int SpecialIndex
        {
            get => _specialIndex;
            set
            {
                _specialIndex = value;
                Cell.IsSpecial = _specialIndex >= 0;
                Cell.RefreshSelfOnly();
            }
        }

        public List<Role> InRoles { get; } = new();

        public List<ButtonDescriptor<WorldCell>> Buttons { get; } = new();


        public int GetResourceType()
        {
            int resourceValue = Stone;
            int resourceType = 0;

            if (Tree > resourceValue)
            {
                resourceValue = Tree;
                resourceType = 1;
            }

            if (Farm > resourceValue)
            {
                resourceValue = Farm;
                resourceType = 2;
            }

            return resourceType;
        }

        public int GetResourceLevel(int type)
        {
            int value = type switch
            {
                0 => Stone,
                1 => Tree,
                2 => Farm,
                _ => Farm
            };

            return Math.Min(value / 1024 - 1, SharedConsts.MaxWorldResourceLevel);
        }

        public void Save(IArchiveWriter writer)
        {
            writer.Write(_specialIndex);
            writer.Write(_stone);
            writer.Write(_tree);
            writer.Write(_farm);
            writer.Write(_pathState);
        }

        public void Load(IArchiveReader reader)
        {
            _specialIndex = reader.ReadInt32();
            _stone = reader.ReadInt32();
            _tree = reader.ReadInt32();
            _farm = reader.ReadInt32();
            _pathState = reader.ReadByte();
        }
    }
}