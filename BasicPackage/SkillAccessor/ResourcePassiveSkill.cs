using System;
using InfinityWorldChess.SkillDomain;
using InfinityWorldChess.Ugf;
using Secyud.Ugf.DataManager;

namespace InfinityWorldChess.SkillAccessor
{
    public class ResourcePassiveSkill:ResourceAccessor<IPassiveSkill>
    {
        
        [field: S,TypeLimit(typeof(IPassiveSkill))]
        public Guid ClassId { get; set; }
        protected override Guid TypeId => ClassId;
    }
}