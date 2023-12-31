﻿using InfinityWorldChess.BattleDomain;
using InfinityWorldChess.Ugf;
using UnityEngine;

namespace InfinityWorldChess.SkillDomain
{
    public class AllTarget : ISkillTargetInRange
    {
        public ISkillTarget GetTargetInRange(BattleUnit battleChess, ISkillRange range)
        {
            return SkillTarget.CreateFromRange(range,
                u => u);
        }

        public void SetContent(Transform transform)
        {
            transform.AddParagraph("目标：所有人。");
        }
    }
}