using System;
using InfinityWorldChess.BattleDomain;
using InfinityWorldChess.SkillDomain.SkillInteractionDomain;

namespace InfinityWorldChess.SkillDomain.TreatDomain
{
	public sealed class TreatRecordBuff : SkillInteractionBuffBase
	{
		public override int Id => -1;
		public float Treat { get; set; }
		
		public float TreatFixedValue { get; set; }

		public float TreatFactor { get; set; } = 1f;
		
		public float RecoverFactor { get; set; } = 1f;
		
		public float RestrainFactor { get; set; } = 0f;
		
		public float TotalRecover { get; set; }
		
		public int TargetCount { get; set; }
		
		public float RunRecover(ICanDefend target)
		{
			float factor = 0.1f + TreatFactor / TargetCount;
			float basic = Treat * factor + TreatFixedValue;
			float recover = basic * RecoverFactor * (1 - RestrainFactor);
			target.HealthValue += recover;
			float totalRecover = recover + Math.Min(target.HealthValue, 0);
			TotalRecover += totalRecover;
			return totalRecover;
		}
	}
}