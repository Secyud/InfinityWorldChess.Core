using System.Collections;
using Secyud.Ugf.DependencyInjection;

namespace InfinityWorldChess.BattleDomain
{
	public interface IBattleAiController:IRegistry
	{
		AiActionNode ResultNode { get; }
		AiControlState State { get; }
		IEnumerator StartPondering();
		void TryInvokeCurrentNode();
	}
}