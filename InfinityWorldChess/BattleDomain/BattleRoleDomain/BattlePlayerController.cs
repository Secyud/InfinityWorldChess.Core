#region

using InfinityWorldChess.BattleDomain.BattleMapDomain;
using InfinityWorldChess.Ugf;
using Secyud.Ugf;
using UnityEngine;

#endregion

namespace InfinityWorldChess.BattleDomain.BattleRoleDomain
{
    public class BattlePlayerController : MonoBehaviour
    {
        private BattleContext _context;
        private CoreSkillActionService _coreSkillActionService;
        private FormSkillActionService _formSkillActionService;

        private void Awake()
        {
            _context = BattleScope.Instance.Context;
        }

        public void OnCoreSkillReset()
        {
            if (_context.Role.ResetCoreSkill())
            {
                _coreSkillActionService.AutoReselectSkill(_context.Role);
            }
            else
            {
                "行动力不足。无法执行操作。".CreateTipFloatingOnCenter();
            }
        }

        public void OnFormSkillReset()
        {
            if (_context.Role.ResetFormSkill())
            {
                _formSkillActionService.AutoReselectSkill(_context.Role);
            }
            else
            {
                "行动力不足。无法执行操作。".CreateTipFloatingOnCenter();
            }
        }

        public void ExitPlayerControl()
        {
            U.Get<BattleControlService>().ExitControl();
        }

        public void FinishBattle()
        {
            U.Get<BattleGlobalService>().DestroyBattle();
        }
    }
}