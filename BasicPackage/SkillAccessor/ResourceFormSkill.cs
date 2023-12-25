using System;
using System.Runtime.InteropServices;
using InfinityWorldChess.SkillDomain;
using InfinityWorldChess.Ugf;
using Secyud.Ugf.DataManager;

namespace InfinityWorldChess.SkillAccessor
{
    [Guid("5CEFD46A-3184-BA0E-0246-B9016849849A")]
    public class ResourceFormSkill:ResourceAccessor<IFormSkill>
    {
        [field: S,TypeLimit(typeof(IFormSkill))]
        public Guid ClassId { get; set; }
        protected override Guid TypeId => ClassId;
        
    }
}