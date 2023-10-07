
using System;
using InfinityWorldChess.BattleDomain.BattleMapDomain;
using InfinityWorldChess.SkillDomain;
using InfinityWorldChess.Ugf;
using Secyud.Ugf;
using UnityEngine;

namespace InfinityWorldChess.BattleDomain.BattleSkillDomain
{
    public class BattleFormSkillCell:BattleSkillCell
    {
        private FormSkillActionService _actionService;
        
        protected override void Awake()
        {
            base.Awake();
            FormSkillContainer formSkill = 
                Context.Role.NextFormSkills[CellIndex];
            SetSkill(formSkill.FormSkill);
            _actionService = U.Get<FormSkillActionService>();
        }

        public void OnClick()
        {
            Context.MapAction = _actionService;
            _actionService.FormSkill = Context.Role.NextFormSkills[CellIndex];
        }
    }
}