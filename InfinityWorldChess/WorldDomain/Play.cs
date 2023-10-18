using Secyud.Ugf;
using Secyud.Ugf.DataManager;
using UnityEngine;

namespace InfinityWorldChess.WorldDomain
{
    public class Play:IShowable
    {
        [field:S]public string ShowDescription { get;set; }
        [field:S]public string ShowName { get;set; }
        [field:S]public IObjectAccessor<Sprite> ShowIcon { get;set; }
        [field:S]public string MapName { get;set; }
        [field:S]public IMapSetting MapSetting { get;set; }
        
    }
}