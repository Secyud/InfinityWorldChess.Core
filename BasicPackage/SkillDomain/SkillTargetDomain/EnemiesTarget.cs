using System.Runtime.InteropServices;
using InfinityWorldChess.BattleDomain;
using InfinityWorldChess.Ugf;
using UnityEngine;

namespace InfinityWorldChess.SkillDomain
{
    [Guid("9A62C533-7D9D-B1BF-9B37-FA016037AE71")]
    public class EnemiesTarget : ISkillTargetInRange
    {
        public static EnemiesTarget Instance { get; } = new();

        public ISkillTarget GetTargetInRange(BattleUnit battleChess, ISkillRange range)
        {
            return SkillTarget.CreateFromRange(range,
                u => u && u.Camp != battleChess.Camp);
        }

        public void SetContent(Transform transform)
        {
            transform.AddParagraph("目标：敌方。");
        }
    }
}