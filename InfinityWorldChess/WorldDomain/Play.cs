using Secyud.Ugf;
using Secyud.Ugf.DataManager;
using UnityEngine;

namespace InfinityWorldChess.WorldDomain
{
    public class Play:IShowable
    {
        [field:S]public string Description { get;set; }
        [field:S]public string Name { get;set; }
        [field:S]public IObjectAccessor<Sprite> Icon { get;set; }
        [field:S]public string MapName { get;set; }
        [field:S]public IMapSetting MapSetting { get;set; }
        
    }
}