using System.Collections.Generic;
using System.Linq;
using InfinityWorldChess.SkillDomain;
using Secyud.Ugf.DataManager;
using UnityEngine;

namespace InfinityWorldChess.SkillFunctions
{
    public class ListInteractionAction : IInteractionAction
    {
        [field: S] public List<IInteractionAction> Actions { get; } = new();

        public ActiveSkillBase Skill
        {
            get => Actions.FirstOrDefault()?.Skill;
            set
            {
                foreach (IInteractionAction action in Actions)
                {
                    action.Skill = value;
                }
            }
        }

        public void SetContent(Transform transform)
        {
            foreach (IInteractionAction action in Actions)
            {
                action.SetContent(transform);
            }
        }

        public void Invoke(SkillInteraction interaction)
        {
            foreach (IInteractionAction action in Actions)
            {
                action.Invoke(interaction);
            }
        }
    }
}