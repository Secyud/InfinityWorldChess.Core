using InfinityWorldChess.SkillDomain.SkillInteractionDomain;
using Secyud.Ugf.DataManager;
using Secyud.Ugf.HexUtilities;

namespace InfinityWorldChess.SkillFunctions.Attack
{
    public class DistanceAddDamage : BasicAttack
    {
        [field: S] public float F256 { get; set; }

        public override string Description=>base.Description+
                                                $"每单位距离增加此招式{F256:P0}伤害。";

        protected override void PreInteraction(SkillInteraction interaction)
        {
            base.PreInteraction(interaction);

            HexCoordinates attackerP = interaction.LaunchChess.Unit.Location.Coordinates;
            HexCoordinates defenderP = interaction.TargetChess.Unit.Location.Coordinates;
            AttackRecord.DamageFactor += F256 * attackerP.DistanceTo(defenderP);
        }
    }
}