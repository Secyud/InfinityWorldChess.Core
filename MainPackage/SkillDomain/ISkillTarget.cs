#region

using System.Collections.Generic;
using InfinityWorldChess.BattleDomain;
using Secyud.Ugf;

#endregion

namespace InfinityWorldChess.SkillDomain
{
	public interface ISkillTarget:IObjectAccessor<List<BattleUnit>>
	{
	}
}