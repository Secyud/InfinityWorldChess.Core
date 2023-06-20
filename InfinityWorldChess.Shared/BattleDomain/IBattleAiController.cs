using System.Collections;

namespace InfinityWorldChess.BattleDomain
{
	public interface IBattleAiController
	{
		AiActionNode ResultNode { get; }
		AiControlState State { get; }
		IEnumerator StartPondering();
		void TryInvokeCurrentNode();
	}
}