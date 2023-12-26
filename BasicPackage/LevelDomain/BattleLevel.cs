using System;
using System.Runtime.InteropServices;
using InfinityWorldChess.BattleTemplates;
using InfinityWorldChess.FunctionDomain;
using InfinityWorldChess.GameDomain;
using InfinityWorldChess.RoleDomain;
using InfinityWorldChess.Ugf;
using Secyud.Ugf;
using Secyud.Ugf.DataManager;
using UnityEngine;

namespace InfinityWorldChess.LevelDomain
{
    [Guid("466B0CA1-E5AA-4497-9D15-FF16A9FE3173")]
    public class BattleLevel : Battle, IBattleLevel
    {
        [field: S(3)] public int Level { get; set; }
    }
}