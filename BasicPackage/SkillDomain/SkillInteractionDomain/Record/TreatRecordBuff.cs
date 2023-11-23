using System;
using InfinityWorldChess.BattleDomain;

namespace InfinityWorldChess.SkillDomain
{
	public sealed class TreatRecordBuff : SkillInteractionBuffBase
	{
		public override int Id => -1;
		public float Treat { get; set; } = 1;
		public float TreatFixedValue { get; set; }
		public float TreatFactor { get; set; } = 1f;
		public float RecoverFactor { get; set; } = 1f;
		public float RestrainFactor { get; set; } = 0f;
		public int TargetCount { get; set; }
		public float TotalRecover { get;private set; }
		public bool IsRecovered { get; private set; } 
		
		
		public float RunRecover(ICanDefend target)
		{
			float factor = 0.1f + O(TreatFactor) / I(TargetCount);
			float basic = O(Treat) * factor + O(TreatFixedValue);
			float recover = basic * O(RecoverFactor) * O(1 - O(RestrainFactor));
			target.HealthValue += recover;
			float totalRecover = recover + Math.Min(target.HealthValue, 0);
			TotalRecover += totalRecover;
			IsRecovered = true;
			return totalRecover;
		}
	}
}