using InfinityWorldChess.BattleDomain;

namespace InfinityWorldChess.SkillDomain
{
	public interface ICoreSkill : IActiveSkill
	{
		ISkillTarget GetTargetInRange(IBattleChess battleChess, ISkillRange range);
		/// <summary>
		///     make sure the code and the layer is matched
		/// </summary>
		byte FullCode { get;set; }

		byte MaxLayer { get;set; }
		
		 byte ConditionCode { get; set; }
		
		 byte ConditionMask { get; set; }
	}
}