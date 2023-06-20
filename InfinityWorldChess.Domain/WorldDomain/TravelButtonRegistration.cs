#region

using InfinityWorldChess.PlayerDomain;
using Secyud.Ugf.ButtonComponents;
using Secyud.Ugf.DependencyInjection;
using Secyud.Ugf.HexMap;
using System.Collections.Generic;

#endregion

namespace InfinityWorldChess.WorldDomain
{
	public class TravelButtonRegistration : ButtonRegistration<HexCell>
	{
		public override bool Visible(HexCell target) => !GameScope.WorldGameContext.Path.IsNullOrEmpty();

		public override void Trigger()
		{
			GameScope.WorldGameContext.Travel(
				GameScope.PlayerGameContext.Unit,
				GameScope.PlayerGameContext.Role);
		}

		public override string ShowName => "旅行";
	}
}