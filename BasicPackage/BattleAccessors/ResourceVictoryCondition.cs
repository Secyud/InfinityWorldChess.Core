using System;
using InfinityWorldChess.BattleDomain;
using InfinityWorldChess.Ugf;
using Secyud.Ugf.DataManager;

namespace InfinityWorldChess.BattleAccessors
{
    public class ResourceVictoryCondition:ResourceAccessor<IBattleVictoryCondition>
    {
        [field: S, TypeLimit(typeof(IBattleVictoryCondition))]
        private Guid ClassId { get; set; }
        protected override Guid TypeId => ClassId;
    }
}