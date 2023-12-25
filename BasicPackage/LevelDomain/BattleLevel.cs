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
    public class BattleLevel : Battle, IBattleLevel,IHasContent
    {
        [field: S(3)] public int Level { get; set; }
        [field: S(18)] public IActionable AwardAction { get; set; }
        
        public override void OnBattleFinished()
        {
            if (Victory)
            {
                Role.BasicProperty basic = GameScope.Instance.Player.Role.Basic;
                int level = Math.Max(0, Level - basic.Level + 0x100);
                basic.Level += level * 0x100;
                AwardAction?.Invoke();
            }
        }

        public void SetContent(Transform transform)
        {
            AwardAction.TrySetContent(transform);
        }
    }
}