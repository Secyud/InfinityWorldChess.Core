#region

using Secyud.Ugf.HexMap;
using Secyud.Ugf.HexMap.Utilities;

#endregion

namespace InfinityWorldChess.BattleDomain
{
	public interface IBattleChess
	{
		BattleCamp Camp { get; }

		RoleBattleChess Belong { get; }

		HexUnit Unit { get; set; }

		bool Active { get; set; }
		HexDirection Direction { get; set; }

		bool Selected { get; set; }

		void SetHighlight();

		int GetSpeed();
	}
}