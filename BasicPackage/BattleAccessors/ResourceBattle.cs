using System;
using InfinityWorldChess.BattleDomain;
using Secyud.Ugf;
using Secyud.Ugf.DataManager;

namespace InfinityWorldChess.BattleAccessors
{
    public class ResourceBattle : IObjectAccessor<IBattleDescriptor>
    {
        [field: S] private string ResourceId { get; set; }

        [field: S, TypeLimit(typeof(IBattleVictoryCondition))]
        private Guid ClassId { get; set; }

        public virtual IBattleDescriptor Value =>
            U.Tm.ConstructFromResource(ClassId, ResourceId) as IBattleDescriptor;
    }
}