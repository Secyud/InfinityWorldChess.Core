#region

using System.Ugf.Collections.Generic;
using InfinityWorldChess.PlayerDomain;
using Secyud.Ugf.ButtonComponents;
using Secyud.Ugf.HexMap;

#endregion

namespace InfinityWorldChess.WorldDomain
{
	public class TravelButtonRegistration : ButtonRegistration<HexCell>
	{
		public override bool Visible(HexCell target) => !GameScope.Instance.World.Path.IsNullOrEmpty();

		public override void Trigger()
		{
			GameScope.Instance.World.Travel(
				GameScope.Instance.Player	.Unit,
				GameScope.Instance.Player.Role);
		}

		public override string ShowName => "旅行";
	}
}