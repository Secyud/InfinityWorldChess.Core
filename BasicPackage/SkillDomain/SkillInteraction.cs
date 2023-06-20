#region

using InfinityWorldChess.BattleDomain;
using InfinityWorldChess.BuffDomain;
using InfinityWorldChess.Ugf;

#endregion

namespace InfinityWorldChess.SkillDomain
{
	public sealed class SkillInteraction : FixTargetBuffProperty<SkillInteraction>
	{
		private SkillInteraction()
		{
		}

		protected override SkillInteraction Target => this;

		public IBattleChess LaunchChess { get; set; }

		public IBattleChess TargetChess { get; set; }


		public static SkillInteraction Get(IBattleChess launch, IBattleChess target)
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
			_launchRecord = LaunchChess?.Belong.GetBattleEvents();
			_launchRecord?.PrepareLaunch.On(this);
			_targetRecord = TargetChess?.Belong.GetBattleEvents();
			_targetRecord?.PrepareReceive.On(this);
		}

		public void AfterHit()
		{
			_targetRecord?.ReceiveCallback.On(this);
			_launchRecord?.LaunchCallback.On(this);
		}
	}
}