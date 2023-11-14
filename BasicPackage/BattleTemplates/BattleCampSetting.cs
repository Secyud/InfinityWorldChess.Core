using System.Collections.Generic;
using Secyud.Ugf.DataManager;

namespace InfinityWorldChess.BattleTemplates
{
    public class BattleCampSetting
    {
        [field: S] public int Index { get; set; }
        [field: S] public string Name { get; set; }
        [field: S] public float R { get; set; }
        [field: S] public float G { get; set; }
        [field: S] public float B { get; set; }
        [field: S] public List<RoleWithIndex> CampRoles { get; } = new();
    }
}