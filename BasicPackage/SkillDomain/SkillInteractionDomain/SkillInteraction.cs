#region

using InfinityWorldChess.BattleDomain;
using InfinityWorldChess.BuffDomain;

#endregion

namespace InfinityWorldChess.SkillDomain.SkillInteractionDomain
{
	/// <summary>
	/// SkillInteraction handle the skill with target.
	/// It inherit buff property so that the interaction can be handled like object.
	/// </summary>
	public sealed class SkillInteraction : IdBuffProperty<SkillInteraction>
	{
		public TypeBuffProperty<SkillInteraction> TypeBuff { get; }
		
		private SkillInteraction()
		{
			TypeBuff = new TypeBuffProperty<SkillInteraction>(this);
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

		private BattleEvents _launchRecord;
		private BattleEvents _targetRecord;

		/// <summary>
		/// before effect hit, some roles has buff to trigger. and target also prepare for it.
		/// </summary>
		public void BeforeHit()
		{
			_launchRecord = LaunchChess?.GetProperty<BattleEvents>();
			_launchRecord?.PrepareLaunch.On(this);
			_targetRecord = TargetChess?.GetProperty<BattleEvents>();
			_targetRecord?.PrepareReceive.On(this);
		}

		/// <summary>
		/// after effect hit, both launcher and target will handle callback.
		/// </summary>
		public void AfterHit()
		{
			_targetRecord?.ReceiveCallback.On(this);
			_launchRecord?.LaunchCallback.On(this);
		}
	}
}