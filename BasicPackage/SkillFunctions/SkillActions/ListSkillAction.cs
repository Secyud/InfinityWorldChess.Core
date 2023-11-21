using System.Collections.Generic;
using System.Linq;
using InfinityWorldChess.BattleDomain;
using InfinityWorldChess.SkillDomain;
using Secyud.Ugf.DataManager;
using UnityEngine;

namespace InfinityWorldChess.SkillFunctions
{
    public class ListSkillAction : ISkillAction
    {
        [field: S] public List<ISkillAction> Actions { get; } = new();

        public ActiveSkillBase Skill
        {
            get => Actions.FirstOrDefault()?.Skill;
            set
            {
                foreach (ISkillAction action in Actions)
                {
                    action.Skill = value;
                }
            }
        }

        public void SetContent(Transform transform)
        {
            foreach (ISkillAction action in Actions)
            {
                action.SetContent(transform);
            }
        }

        public void Invoke(BattleRole battleChess, BattleCell releasePosition)
        {
            foreach (ISkillAction action in Actions)
            {
                action.Invoke(battleChess, releasePosition);
            }
        }
    }
}