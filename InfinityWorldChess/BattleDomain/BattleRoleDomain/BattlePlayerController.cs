#region

using InfinityWorldChess.Ugf;
using Secyud.Ugf;
using UnityEngine;

#endregion

namespace InfinityWorldChess.BattleDomain
{
    public class BattlePlayerController : MonoBehaviour
    {
        private BattleContext _context;
        private CoreSkillActionService _coreSkillActionService;
        private FormSkillActionService _formSkillActionService;

        private void Awake()
        {
            _context = BattleScope.Instance.Context;
            _coreSkillActionService = U.Get<CoreSkillActionService>();
            _formSkillActionService = U.Get<FormSkillActionService>();
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

        public void SelectMoveButton()
        {
            _context.MapAction = U.Get<MoveActionService>();
        }

        public void ExitPlayerControl()
        {
            U.Get<BattleControlService>().ExitControl();
        }

        public void FinishBattle()
        {
            BattleScope.DestroyBattle();
        }
    }
}