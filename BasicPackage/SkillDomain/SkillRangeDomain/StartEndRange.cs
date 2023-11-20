using InfinityWorldChess.Ugf;
using Secyud.Ugf;
using Secyud.Ugf.DataManager;
using UnityEngine;

namespace InfinityWorldChess.SkillDomain.SkillRangeDomain
{
    public class StartEndRange : IHasContent
    {
        [field: S(-1)] public byte Start { get; set; }
        [field: S(-1)] public byte End { get; set; }

        protected virtual string SeLabel => $"({Start},{End})";

        public virtual string Description => null;

        public virtual void SetContent(Transform transform)
        {
            transform.AddParagraph(Description + SeLabel);
        }
    }
}