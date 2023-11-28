#region

using System.Collections.Generic;
using InfinityWorldChess.GameDomain.WorldMapDomain;
using InfinityWorldChess.RoleDomain;
using Secyud.Ugf.HexMapExtensions;
using Secyud.Ugf.TableComponents.ButtonComponents;
using Secyud.Ugf.UgfHexMap;
using UnityEngine;

#endregion

namespace InfinityWorldChess.GameDomain.WorldCellDomain
{
    public class WorldCell : UgfCell
    {
        private byte _pathState;
        private WorldCellMessage _message;
        public List<Role> InRoles { get; } = new();
        public List<ButtonDescriptor<WorldCell>> Buttons { get; } = new();

        public WorldCellMessage Message
        {
            get => _message;
            set
            {
                _message = value;

                FeaturePrefab = value?.FeaturePrefab?.Value;
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