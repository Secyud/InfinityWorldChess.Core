using System;
using InfinityWorldChess.SkillDomain;
using InfinityWorldChess.Ugf;
using Secyud.Ugf.DataManager;

namespace InfinityWorldChess.SkillTemplates
{
    public class ResourceBattleBuff : ResourceAccessor<SkillBuff>
    {
        [field: S, TypeLimit(typeof(SkillBuff))]
        public Guid ClassId { get; set; }

        protected override Guid TypeId => ClassId;
    }
}