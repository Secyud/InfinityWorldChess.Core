using InfinityWorldChess.SkillDomain;
using Secyud.Ugf.DataManager;
using UnityEngine;

namespace InfinityWorldChess.SkillFunctions
{
    public class SkillActionDelegate : ISkillInteractionEffect
    {
        [field: S] public ISkillActionEffect SkillActionEffect { get; set; }

        public ActiveSkillBase BelongSkill { get; set; }

        public void SetContent(Transform transform)
        {
            SkillActionEffect?.SetContent(transform);
        }

        public void Invoke(SkillInteraction interaction)
        {
            SkillActionEffect?.Invoke(BelongSkill.Role, BelongSkill.Cell);
        }
    }
}