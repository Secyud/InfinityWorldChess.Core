using System;
using System.Runtime.InteropServices;
using InfinityWorldChess.BattleBuffDomain;
using InfinityWorldChess.Ugf;
using Secyud.Ugf.DataManager;

namespace InfinityWorldChess.SkillTemplates
{
    [Guid("C2F2F5BA-8126-329C-4AAF-69896E43B71E")]
    public class ResourceBattleBuff : ResourceAccessor<BattleUnitBuff>
    {
        [field: S, TypeLimit(typeof(BattleUnitBuff))]
        public Guid ClassId { get; set; }

        protected override Guid TypeId => ClassId;
    }
}