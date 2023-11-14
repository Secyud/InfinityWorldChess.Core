using System;
using InfinityWorldChess.SkillDomain;
using InfinityWorldChess.Ugf;
using Secyud.Ugf.DataManager;

namespace InfinityWorldChess.SkillAccessor
{
    public class ResourceCoreSkill:ResourceAccessor<ICoreSkill>
    {
        [field: S,TypeLimit(typeof(ICoreSkill))]
        public Guid ClassId { get; set; }
        protected override Guid TypeId => ClassId;
    }
}