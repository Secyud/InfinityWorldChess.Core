using System;
using System.Runtime.InteropServices;
using InfinityWorldChess.BattleDomain;
using InfinityWorldChess.Ugf;
using Secyud.Ugf.DataManager;

namespace InfinityWorldChess.BattleAccessors
{
    [Guid("E381F963-B528-3639-9B55-74CB03178E20")]
    public class ResourceBattle :ResourceAccessor<IBattleDescriptor>
    {
        [field: S, TypeLimit(typeof(IBattleDescriptor))]
        private Guid ClassId { get; set; }
        protected override Guid TypeId => ClassId;
    }
}