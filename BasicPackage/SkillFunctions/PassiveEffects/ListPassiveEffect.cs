using System.Collections.Generic;
using System.Linq;
using InfinityWorldChess.BattleDomain;
using InfinityWorldChess.RoleDomain;
using InfinityWorldChess.SkillDomain;
using Secyud.Ugf.DataManager;
using UnityEngine;

namespace InfinityWorldChess.SkillFunctions
{
    public class ListPassiveEffect : IPassiveSkillAttached
    {
        [field: S] public List<IPassiveSkillAttached> Actions { get; } = new();

        public PassiveSkill Skill
        {
            get => Actions.FirstOrDefault()?.Skill;
            set
            {
                foreach (IPassiveSkillAttached action in Actions)
                {
                    action.Skill = value;
                }
            }
        }

        public void SetContent(Transform transform)
        {
            foreach (IPassiveSkillAttached action in Actions)
            {
                action.SetContent(transform);
            }
        }

        public void Install(Role target)
        {
            foreach (IPassiveSkillAttached action in Actions)
            {
                action.Install(target);
            }
        }

        public void UnInstall(Role target)
        {
            foreach (IPassiveSkillAttached action in Actions)
            {
                action.UnInstall(target);
            }
        }
    }
}