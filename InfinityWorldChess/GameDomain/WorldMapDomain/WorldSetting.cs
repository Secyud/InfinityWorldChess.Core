using System.Collections.Generic;
using System.IO;
using InfinityWorldChess.GameDomain.WorldCellDomain;
using InfinityWorldChess.PlayerDomain;
using Secyud.Ugf;
using Secyud.Ugf.Archiving;
using Secyud.Ugf.DataManager;
using UnityEngine;

namespace InfinityWorldChess.GameDomain.WorldMapDomain
{
    public sealed class WorldSetting : IShowable,IDataResource
    {
        [field: S] public string Description { get; set; }
        [field: S] public string Name { get; set; }
        [field: S] public IObjectAccessor<Sprite> Icon { get; set; }
        [field: S] public int PlayerCellIndex { get; set; }

        [field: S] public string ResourceId { get; set; }

        public void PreparePlayer(PlayerGameContext context)
        {
            context.Role.Position = GameScope.Instance.Map.Value.GetCell(PlayerCellIndex) as WorldCell;
        }

        public string GetDataDirectory()
        {
            return Path.Combine(U.Path,"Data/Play",Name);
        }
    }
}