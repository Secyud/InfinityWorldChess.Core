using System;
using InfinityWorldChess.BattleDomain;
using InfinityWorldChess.Ugf;
using Secyud.Ugf.DataManager;

namespace InfinityWorldChess.BattleAccessors
{
    public class ResourceBattle :ResourceAccessor<IBattleDescriptor>
    {
        [field: S, TypeLimit(typeof(IBattleDescriptor))]
        private Guid ClassId { get; set; }
        protected override Guid TypeId => ClassId;
    }
}