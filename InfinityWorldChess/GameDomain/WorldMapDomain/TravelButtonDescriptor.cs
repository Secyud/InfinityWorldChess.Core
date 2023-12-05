#region

using System;
using System.Collections.Generic;
using System.Linq;
using System.Ugf.Collections.Generic;
using InfinityWorldChess.GameDomain.WorldCellDomain;
using Secyud.Ugf;
using Secyud.Ugf.TableComponents.ButtonComponents;

#endregion

namespace InfinityWorldChess.GameDomain.WorldMapDomain
{
    public class TravelButtonDescriptor : ButtonDescriptor<WorldCell>
    {
        public override bool Visible(WorldCell target) =>
            !GameScope.Instance.Map.Path.IsNullOrEmpty();

        public override void Invoke()
        {
            GameScope scope = GameScope.Instance;
            IList<int> path = scope.Map.Path;
            WorldMapFunctionService service = U.Get<WorldMapFunctionService>();
            service.Travel();
            int last = path.Last();
            scope.Get<CurrentTabService>().Cell = scope.GetCell(last);
            scope.Map.Path = Array.Empty<int>();
        }

        public override string Name => "旅行";
    }
}