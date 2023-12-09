using System.Collections;
using System.Collections.Generic;
using Secyud.Ugf.DependencyInjection;

namespace InfinityWorldChess.BattleDomain
{
	public interface IBattleAiController:IRegistry
	{
		IEnumerator StartPondering();
	}
}