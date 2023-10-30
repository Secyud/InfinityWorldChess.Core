using System.Collections.Generic;
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
        [field: S] public List<WorldCellMessage> Messages { get; } = new();


        public void PrepareWorld(WorldGameContext context)
        {
            foreach (WorldCellMessage message in Messages)
            {
                context.AddMessage(message);
            }
        }

        public void PreparePlayer(PlayerGameContext context)
        {
            context.Role.Position = GameScope.Instance.Map.Value.GetCell(PlayerCellIndex) as WorldCell;
        }
    }
}