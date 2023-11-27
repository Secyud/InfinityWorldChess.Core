using System;
using InfinityWorldChess.SkillDomain;
using InfinityWorldChess.Ugf;
using Secyud.Ugf.DataManager;

namespace InfinityWorldChess.SkillAccessor
{
    public class ResourceFormSkill:ResourceAccessor<IFormSkill>
    {
        [field: S,TypeLimit(typeof(IFormSkill))]
        public Guid ClassId { get; set; }
        protected override Guid TypeId => ClassId;
        
    }
}