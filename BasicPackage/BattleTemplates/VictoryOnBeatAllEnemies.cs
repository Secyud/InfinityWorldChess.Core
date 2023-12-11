#region

using System.Linq;
using InfinityWorldChess.BattleDomain;

#endregion

namespace InfinityWorldChess.BattleTemplates
{
    public class VictoryOnBeatAllEnemies : IBattleVictoryCondition
    {
        private BattleContext _context;

        public string Description => "击败所有不同阵营的角色.";

        public void SetCondition()
        {
            BattleScope.Instance.Context.ChessRemoveAction += CheckVictory;
        }

        public bool Victory { get; private set; }

        public bool Defeated { get; private set; }

        public void CheckVictory(BattleRole role)
        {
            bool victory = true;
            bool defeated = true;

            if (role.Camp.Index <= 0)
                return;

            foreach (BattleRole chess in BattleScope.Instance.Context.BattleRoles
                         .Where(chess => chess.Camp is not null && !chess.Dead))
            {
                if (chess.Camp.Index == 0)
                    defeated = false;
                else
                    victory = false;
                if (!defeated && !victory)
                    return;
            }

            Victory = victory;
            Defeated = defeated;
        }
    }
}