using InfinityWorldChess.SkillDomain;
using Secyud.Ugf.HexMap.Utilities;
using Secyud.Ugf.Resource;

namespace InfinityWorldChess.BasicBundle.CoreSkills
{
    public class DistanceAddDamage : CoreSkillTemplate
    {
        [R(256)] public float F256 { get; set; }

        protected override string HDescription =>
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