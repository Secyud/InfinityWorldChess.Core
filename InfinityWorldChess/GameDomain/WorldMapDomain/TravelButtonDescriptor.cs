#region

using System.Collections.Generic;
using System.Linq;
using System.Ugf.Collections.Generic;
using InfinityWorldChess.GameDomain.WorldCellDomain;
using Secyud.Ugf.HexMap;
using Secyud.Ugf.TableComponents.ButtonComponents;

#endregion

namespace InfinityWorldChess.GameDomain.WorldMapDomain
{
    public class TravelButtonDescriptor : ButtonDescriptor<HexCell>
    {
        public override bool Visible(HexCell target) =>
            !WorldGameContext.Map.Path.IsNullOrEmpty();

        public override void Trigger()
        {
            HexUnit player = GameScope.Instance.Player.Unit;
            List<HexCell> path = WorldGameContext.Map.Path;
            player.Travel(path);
            HexCell last = path.Last();
            player.Location = last;
            GameScope.Instance.Get<CurrentTabService>().Cell = last.Get<WorldCell>();
            WorldGameContext.Map.Path = null;
        }

        public override string ShowName => "旅行";
    }
}