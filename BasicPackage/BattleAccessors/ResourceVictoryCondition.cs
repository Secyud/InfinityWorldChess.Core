using System;
using InfinityWorldChess.BattleDomain;
using Secyud.Ugf;
using Secyud.Ugf.DataManager;

namespace InfinityWorldChess.BattleAccessors
{
    public class ResourceVictoryCondition:IObjectAccessor<IBattleVictoryCondition>
    {
        [field: S] private string ResourceId { get; set; }

        [field: S, TypeLimit(typeof(IBattleVictoryCondition))]
        private Guid ClassId { get; set; }

        public virtual IBattleVictoryCondition Value =>
            U.Tm.ConstructFromResource(ClassId, ResourceId) as IBattleVictoryCondition;
    }
}