using System.Collections.Generic;
using Secyud.Ugf.DataManager;

namespace InfinityWorldChess.BattleTemplates
{
    public class BattleCampSetting
    {
        [field: S(0)] public int Index { get; set; }
        [field: S(0)] public string Name { get; set; }
        [field: S(1)] public float R { get; set; }
        [field: S(1)] public float G { get; set; }
        [field: S(1)] public float B { get; set; }
        [field: S(2)] public List<RoleWithPosition> CampRoles { get; } = new();
    }
}