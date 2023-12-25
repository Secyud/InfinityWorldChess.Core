using System.Collections.Generic;
using System.Runtime.InteropServices;
using Secyud.Ugf.DataManager;

namespace InfinityWorldChess.BattleTemplates
{
    [Guid("91534ADD-02ED-92C5-7269-2D1D8F1C297B")]
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