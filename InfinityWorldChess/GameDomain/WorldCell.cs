#region

using System;
using System.Collections.Generic;
using InfinityWorldChess.RoleDomain;
using Secyud.Ugf.Archiving;
using Secyud.Ugf.TableComponents.ButtonComponents;
using Secyud.Ugf.UgfHexMap;
using UnityEngine;

#endregion

namespace InfinityWorldChess.GameDomain
{
    public class WorldCell : UgfCell
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

        public void SetHighlight()
        {
            switch (_pathState)
            {
                case 1:
                    EnableHighlight(PathCellColor);
                    break;
                case 2:
                    EnableHighlight(FromCellColor);
                    break;
                case 3:
                    EnableHighlight(ToCellColor);
                    break;
                default:
                    DisableHighlight();
                    break;
            }
        }

        public Color PathCellColor => Color.yellow;
        public Color FromCellColor => Color.blue;
        public Color ToCellColor => Color.green;

        public int Stone
        {
            get => _stone;
            set
            {
                _stone = value;
                RefreshSelfOnly();
            }
        }

        public int Tree
        {
            get => _tree;
            set
            {
                _tree = value;
                RefreshSelfOnly();
            }
        }

        public int Farm
        {
            get => _farm;
            set
            {
                _farm = value;
                RefreshSelfOnly();
            }
        }

        public int SpecialIndex
        {
            get => _specialIndex;
            set
            {
                _specialIndex = value;
                RefreshSelfOnly();
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

        public override void Save(IArchiveWriter writer)
        {
            base.Save(writer);
            writer.Write(_specialIndex);
            writer.Write(_stone);
            writer.Write(_tree);
            writer.Write(_farm);
            writer.Write(_pathState);
        }

        public override void Load(IArchiveReader reader)
        {
            base.Load(reader);
            _specialIndex = reader.ReadInt32();
            _stone = reader.ReadInt32();
            _tree = reader.ReadInt32();
            _farm = reader.ReadInt32();
            _pathState = reader.ReadByte();
        }
    }
}