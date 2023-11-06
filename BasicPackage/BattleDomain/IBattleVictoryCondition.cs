using Secyud.Ugf;

namespace InfinityWorldChess.BattleDomain
{
    public interface IBattleVictoryCondition:IHasDescription
    {
         void SetCondition();
        
         bool Victory { get; }

         bool Defeated { get;  }
    }
}