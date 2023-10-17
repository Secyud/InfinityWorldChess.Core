#region

using System.Collections.Generic;
using InfinityWorldChess.RoleDomain;
using Secyud.Ugf.Archiving;
using Secyud.Ugf.TableComponents.ButtonComponents;
using Secyud.Ugf.UgfHexMap;
using UnityEngine;

#endregion

namespace InfinityWorldChess.GameDomain.WorldCellDomain
{
    public class WorldCell : UgfCell
    {
        private int _specialIndex = -1;
        private byte _pathState;
        public List<Role> InRoles { get; } = new();
        public List<ButtonDescriptor<WorldCell>> Buttons { get; } = new();

        public int Stone { get; set; }
        public int Tree { get; set; }
        public int Farm { get; set; }

        public int SpecialIndex
        {
            get => _specialIndex;
            set
            {
                _specialIndex = value;
                RefreshSelfOnly();
            }
        }

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
                    //pathCellColor
                    EnableHighlight(Color.yellow);
                    break;
                case 2:
                    //fromCellColor
                    EnableHighlight(Color.blue);
                    break;
                case 3:
                    //toCellColor
                    EnableHighlight(Color.green);
                    break;
                default:
                    DisableHighlight();
                    break;
            }
        }

        public override void Save(IArchiveWriter writer)
        {
            base.Save(writer);
            writer.Write(_specialIndex);
            writer.Write(Stone);
            writer.Write(Tree);
            writer.Write(Farm);
        }

        public override void Load(IArchiveReader reader)
        {
            base.Load(reader);
            _specialIndex = reader.ReadInt32();
            Stone = reader.ReadInt32();
            Tree = reader.ReadInt32();
            Farm = reader.ReadInt32();
        }
    }
}