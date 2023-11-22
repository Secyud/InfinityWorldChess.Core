using System.Collections.Generic;
using System.Linq;
using InfinityWorldChess.BattleDomain;
using InfinityWorldChess.SkillDomain;
using Secyud.Ugf.DataManager;
using UnityEngine;

namespace InfinityWorldChess.SkillFunctions
{
    public class ListSkillActionEffect : ISkillActionEffect
    {
        [field: S] public List<ISkillActionEffect> Actions { get; } = new();

        public ActiveSkillBase BelongSkill
        {
            get => Actions.FirstOrDefault()?.BelongSkill;
            set
            {
                foreach (ISkillActionEffect action in Actions)
                {
                    action.BelongSkill = value;
                }
            }
        }

        public void SetContent(Transform transform)
        {
            foreach (ISkillActionEffect action in Actions)
            {
                action.SetContent(transform);
            }
        }

        public void Invoke(BattleRole battleChess, BattleCell releasePosition)
        {
            foreach (ISkillActionEffect action in Actions)
            {
                action.Invoke(battleChess, releasePosition);
            }
        }
    }
}