#region

using System.Collections.Generic;
using InfinityWorldChess.GameDomain.WorldMapDomain;
using InfinityWorldChess.RoleDomain;
using Secyud.Ugf.AssetComponents;
using Secyud.Ugf.HexMapExtensions;
using Secyud.Ugf.TableComponents.ButtonComponents;
using Secyud.Ugf.UgfHexMap;
using UnityEngine;

#endregion

namespace InfinityWorldChess.GameDomain.WorldCellDomain
{
    public class WorldCell : UgfCell
    {
        private static readonly PrefabContainer<Transform> PrefabContainer =
            PrefabContainer<Transform>.Create<IwcAssets>("Features/Special/Village.prefab");

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

                if (value is not null)
                {
                    FeaturePrefab = value.FeaturePrefab is not null
                        ? value.FeaturePrefab.Value
                        : PrefabContainer.Value;
                }
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