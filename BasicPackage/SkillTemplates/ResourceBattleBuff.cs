using System;
using InfinityWorldChess.BattleBuffDomain;
using InfinityWorldChess.Ugf;
using Secyud.Ugf.DataManager;

namespace InfinityWorldChess.SkillTemplates
{
    public class ResourceBattleBuff : ResourceAccessor<BattleRoleBuff>
    {
        [field: S, TypeLimit(typeof(BattleRoleBuff))]
        public Guid ClassId { get; set; }

        protected override Guid TypeId => ClassId;
    }
}