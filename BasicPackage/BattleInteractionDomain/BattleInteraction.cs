#region

using System.Linq;
using InfinityWorldChess.BattleDomain;
using InfinityWorldChess.BuffDomain;
using InfinityWorldChess.FunctionDomain;

#endregion

namespace InfinityWorldChess.BattleInteractionDomain
{
    /// <summary>
    /// SkillInteraction handle the skill with target.
    /// It inherit buff property so that the interaction can be handled like object.
    /// </summary>
    public sealed class BattleInteraction 
    {
        private BattleEvents _originRecord;
        private BattleEvents _targetRecord;
        public PropertyCollection<BattleInteraction, BattleInteractionPropertyBase> Properties { get; }
        public BattleUnit Origin { get; set; }
        public BattleUnit Target { get; set; }

        private BattleInteraction()
        {
            Properties = new PropertyCollection<BattleInteraction, BattleInteractionPropertyBase>(this);
        }


        public static BattleInteraction Create(BattleUnit launch, BattleUnit target)
        {
            return new BattleInteraction
            {
                Origin = launch,
                Target = target
            };
        }

        /// <summary>
        /// before effect hit, some roles has buff to trigger. and target also prepare for it.
        /// </summary>
        public void BeforeHit()
        {
            if (Origin)
            {
                _originRecord = Origin.Properties.GetOrCreate<BattleEvents>();
                _originRecord?.PrepareLaunch.ToList().InvokeList(this);
            }

            if (Target)
            {
                _targetRecord = Target.Properties.GetOrCreate<BattleEvents>();
                _targetRecord?.PrepareReceive.ToList().InvokeList(this);
            }
        }

        /// <summary>
        /// after effect hit, both launcher and target will handle callback.
        /// </summary>
        public void AfterHit()
        {
            _targetRecord?.ReceiveCallback.ToList().InvokeList(this);
            _originRecord?.LaunchCallback.ToList().InvokeList(this);
        }
    }
}