using System.Collections.Generic;
using InfinityWorldChess.BattleDomain;
using InfinityWorldChess.FunctionDomain;
using InfinityWorldChess.ItemTemplates;
using InfinityWorldChess.RoleDomain;
using Secyud.Ugf.DataManager;

namespace InfinityWorldChess.ManufacturingFunctions
{
    public class AddBattleUnitActionable
    {
        [field:S] public IActionable<BattleUnit> BattleRoleActionAble { get; set; }

        protected void Add(Consumable consumable)
        {
            consumable.EffectsInBattle.Add(BattleRoleActionAble);
        }
    }
}