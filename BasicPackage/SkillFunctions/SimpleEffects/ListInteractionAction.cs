using System.Collections.Generic;
using System.Linq;
using InfinityWorldChess.SkillDomain;
using Secyud.Ugf.DataManager;
using UnityEngine;

namespace InfinityWorldChess.SkillFunctions
{
    public class ListInteractionAction : ISkillInteractionEffect
    {
        [field: S] public List<ISkillInteractionEffect> Actions { get; } = new();

        public ActiveSkillBase BelongSkill
        {
            get => Actions.FirstOrDefault()?.BelongSkill;
            set
            {
                foreach (ISkillInteractionEffect action in Actions)
                {
                    action.BelongSkill = value;
                }
            }
        }

        public void SetContent(Transform transform)
        {
            foreach (ISkillInteractionEffect action in Actions)
            {
                action.SetContent(transform);
            }
        }

        public void Invoke(SkillInteraction interaction)
        {
            foreach (ISkillInteractionEffect action in Actions)
            {
                action.Invoke(interaction);
            }
        }
    }
}