#region

using UnityEngine;

#endregion

namespace InfinityWorldChess.BattleDomain
{
	public class BattleUiComponent : MonoBehaviour
	{
		public BattleCheckerMessage BattleCheckerMessage;
		public BattlePlayerController BattlePlayerController;
		public BattleRoleMessage BattleRoleActiveMessage;
		public BattleChessMessage BattleChessSelectMessage;
		public BattleFloatingBroadcast BattleFloatingBroadcast;
	}
}