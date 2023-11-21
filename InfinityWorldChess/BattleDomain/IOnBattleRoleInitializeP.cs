using InfinityWorldChess.BuffDomain;
using Secyud.Ugf;

namespace InfinityWorldChess.BattleDomain
{
    public interface IOnBattleRoleInitializeP: IOnBattleRoleInitialize,IHasContent
    {
        public IBuffProperty Property { get; set; }
    }
}