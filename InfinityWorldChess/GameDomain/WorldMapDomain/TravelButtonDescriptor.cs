﻿#region

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
            !WorldGameContext.Map.Path.IsNullOrEmpty();

        public override void Invoke()
        {
            IList<WorldCell> path = WorldGameContext.Map.Path;
            WorldMapFunctionService service = 
                U.Get<WorldMapFunctionService>();
            service.Travel();
            WorldCell last = path.Last();
            GameScope.Instance.Get<CurrentTabService>().Cell = last;
            WorldGameContext.Map.Path = Array.Empty<WorldCell>();
        }

        public override string Name => "旅行";
    }
}