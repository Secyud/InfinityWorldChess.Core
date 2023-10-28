using InfinityWorldChess.Ugf;
using Secyud.Ugf;
using Secyud.Ugf.DataManager;
using UnityEngine;

namespace InfinityWorldChess.SkillDomain.SkillRangeDomain
{
    public class StartEndRange: IHasContent
    {
        [field: S ] public byte Start { get; set; }
        [field: S ] public byte End { get; set; }

        protected virtual string SeLabel => $"({Start},{End})";

        public virtual string Description => null;
        
        public virtual void SetContent(Transform transform)
        {
            transform.AddParagraph(Description + SeLabel);
        }
    }
}