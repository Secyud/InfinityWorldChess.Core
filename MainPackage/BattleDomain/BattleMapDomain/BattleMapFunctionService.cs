using System.Collections;
using System.Collections.Generic;
using InfinityWorldChess.GameDomain;
using Secyud.Ugf.DependencyInjection;
using Secyud.Ugf.HexMap;
using Secyud.Ugf.UgfHexMap;

namespace InfinityWorldChess.BattleDomain
{
    [Registry(DependScope = typeof(GameScope))]
    public class BattleMapFunctionService:
        UgfHexMapFunctionService<BattleHexMapMessageService>
    {
        public BattleMapFunctionService(BattleHexMapMessageService service) : base(service)
        {
        }

        protected override IEnumerator TravelPath(IList<int> path, HexUnit unit)
        {
            yield return base.TravelPath(path, unit);
            
            BattleScope.Instance.Get<BattleControlService>().EnterControl();
        }
    }
}