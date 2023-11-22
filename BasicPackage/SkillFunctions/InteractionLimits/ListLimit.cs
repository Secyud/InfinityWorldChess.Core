using System.Collections.Generic;
using System.Linq;
using InfinityWorldChess.SkillDomain;
using Secyud.Ugf;
using Secyud.Ugf.DataManager;
using UnityEngine;

namespace InfinityWorldChess.SkillFunctions.InteractionLimits
{
    public class ListLimit : ILimitCondition, IHasContent
    {
        [field: S] public List<ILimitCondition> Limits { get; } = new();

        public bool CheckLimit(object sender)
        {
            return Limits.All(limit => limit.CheckLimit(sender));
        }

        public void SetContent(Transform transform)
        {
            foreach (ILimitCondition limit in Limits)
            {
                if (limit is IHasContent content)
                {
                    content.SetContent(transform);
                }
            }
        }
    }
}