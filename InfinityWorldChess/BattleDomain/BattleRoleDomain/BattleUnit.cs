using System;
using Secyud.Ugf.HexMap;

namespace InfinityWorldChess.BattleDomain
{
    public class BattleUnit:HexUnit
    {
        public BattleRoleStateViewer StateViewer { get; set; }

        public override void Die()
        {
            base.Die();
            Destroy(StateViewer.gameObject);
        }
    }
}