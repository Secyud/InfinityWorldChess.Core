using System;
using System.Runtime.InteropServices;
using InfinityWorldChess.SkillDomain;
using InfinityWorldChess.Ugf;
using Secyud.Ugf.DataManager;

namespace InfinityWorldChess.SkillAccessor
{
    [Guid("B54E1241-41BD-C81A-D0CA-CC57CB1F94DC")]
    public class ResourceCoreSkill:ResourceAccessor<ICoreSkill>
    {
        [field: S,TypeLimit(typeof(ICoreSkill))]
        public Guid ClassId { get; set; }
        protected override Guid TypeId => ClassId;
    }
}