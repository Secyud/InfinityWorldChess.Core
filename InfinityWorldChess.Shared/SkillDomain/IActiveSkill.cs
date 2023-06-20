#region

using InfinityWorldChess.GlobalDomain;
using Secyud.Ugf;
using Secyud.Ugf.HexMap;
using UnityEngine.Playables;

#endregion

namespace InfinityWorldChess.SkillDomain
{
	public interface IActiveSkill : 
		ISkillCastCondition, ISkillCastPosition, ISkillCastResult, ISkillCastEffect,ISkill
	{
		SkillTargetType TargetType { get; }

		bool Damage { get; }
	}
}