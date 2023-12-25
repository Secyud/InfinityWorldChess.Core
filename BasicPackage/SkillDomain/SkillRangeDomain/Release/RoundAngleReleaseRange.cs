using System.Runtime.InteropServices;
using InfinityWorldChess.BattleDomain;
using InfinityWorldChess.Ugf;
using Secyud.Ugf.DataManager;
using UnityEngine;

namespace InfinityWorldChess.SkillDomain.Release
{
    [Guid("922AEA90-8ECB-F057-CF77-6A5997D8BE4D")]
    public class RoundAngleReleaseRange : StartEndRange, ISkillCastPosition
    {
        [field: S] public bool IncludeUnit { get; set; }
        protected override string Description => "圆形";

        public ISkillRange GetCastPositionRange(BattleUnit unit, IActiveSkill skill)
        {
            return SkillRange.RoundAngle(
                Start, End, unit.Location.Coordinates, IncludeUnit);
        }

        public void SetContent(Transform transform)
        {
            transform.AddParagraph($"释放范围：圆形({Start}-{End})");
        }
    }
}