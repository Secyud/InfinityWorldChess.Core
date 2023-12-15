using System;
using InfinityWorldChess.BattleDomain;

namespace InfinityWorldChess.BattleInteractionDomain
{
	public sealed class TreatRecordProperty : BattleInteractionPropertyBase
	{
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
			float factor = 0.1f + O(TreatFactor) / O(TargetCount,1);
			float basic = O(Treat) * factor + O(TreatFixedValue);
			float recover = basic * O(RecoverFactor,0.1f) * O(1 - O(RestrainFactor),0.1f);
			target.HealthValue += recover;
			float totalRecover = recover + Math.Min(target.HealthValue, 0);
			TotalRecover += totalRecover;
			IsRecovered = true;
			return totalRecover;
		}
	}
}