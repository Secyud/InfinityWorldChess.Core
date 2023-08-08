#region

using InfinityWorldChess.BattleDomain;
using InfinityWorldChess.BuffDomain;
using InfinityWorldChess.Ugf;

#endregion

namespace InfinityWorldChess.SkillDomain
{
	public sealed class SkillInteraction : BuffProperty<SkillInteraction>
	{
		private SkillInteraction()
		{
		}

		protected override SkillInteraction Target => this;

		public BattleRole LaunchChess { get; set; }

		public BattleRole TargetChess { get; set; }


		public static SkillInteraction Get(BattleRole launch, BattleRole target)
		{
			return new SkillInteraction
			{
				LaunchChess = launch,
				TargetChess = target
			};
		}

		private BattleEventsBuff _launchRecord;
		private BattleEventsBuff _targetRecord;

		public void BeforeHit()
		{
			_launchRecord = LaunchChess?.GetBattleEvents();
			_launchRecord?.PrepareLaunch.On(this);
			_targetRecord = TargetChess?.GetBattleEvents();
			_targetRecord?.PrepareReceive.On(this);
		}

		public void AfterHit()
		{
			_targetRecord?.ReceiveCallback.On(this);
			_launchRecord?.LaunchCallback.On(this);
		}
	}
}