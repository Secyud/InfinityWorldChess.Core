#region

using System.Linq;
using System.Runtime.InteropServices;
using InfinityWorldChess.BattleDomain;

#endregion

namespace InfinityWorldChess.BattleTemplates
{
    [Guid("A00DA8E0-04B7-2B80-F604-9AD60094C6B6")]
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

        public void CheckVictory(BattleUnit unit)
        {
            bool victory = true;
            bool defeated = true;

            if (unit.Camp.Index <= 0)
                return;

            foreach (BattleUnit chess in BattleScope.Instance.Context.Units
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