using InfinityWorldChess.BattleDomain;
using InfinityWorldChess.SkillDomain;
using Secyud.Ugf.HexMap;
using Secyud.Ugf.HexMap.Utilities;

namespace InfinityWorldChess.BasicBundle.CoreSkills
{
    /// <summary>
    /// 击退一格，若失败，则再攻击一次
    /// </summary>
    public class RepelOrAttackMore : CoreSkillTemplate
    {
        protected override string HDescription =>
            "击退敌方一格，若因阻挡而无法击退，则再次对敌方造成伤害。";
        protected override void PostInteraction(SkillInteraction interaction)
        {
            base.PostInteraction(interaction);
            HexCell lC = interaction.LaunchChess.Unit.Location;
            HexCell rC = interaction.TargetChess.Unit.Location;
            HexDirection direction = rC.DirectionTo(lC);
            HexCell neighbour = rC.GetNeighbor(direction);
            if (neighbour && !neighbour.Unit)
                interaction.TargetChess.Unit.Location = neighbour;
            else if (interaction.TargetChess is ICanDefend defender)
                AttackRecord.RunDamage(defender);
        }
    }
}