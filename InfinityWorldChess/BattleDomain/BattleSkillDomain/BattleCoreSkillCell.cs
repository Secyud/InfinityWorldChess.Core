using System;
using InfinityWorldChess.BattleDomain.BattleMapDomain;
using InfinityWorldChess.SkillDomain;
using InfinityWorldChess.Ugf;
using Secyud.Ugf;
using Secyud.Ugf.BasicComponents;
using UnityEngine;

namespace InfinityWorldChess.BattleDomain.BattleSkillDomain
{
    public class BattleCoreSkillCell:BattleSkillCell
    {
        private CoreSkillActionService _actionService;
        
        protected override void Awake()
        {
            base.Awake();
            FormSkillContainer formSkill = 
                Context.Role.NextFormSkills[CellIndex];
            SetSkill(formSkill.FormSkill);
            _actionService = U.Get<CoreSkillActionService>();
        }

        public void OnClick()
        {
            Context.MapAction = _actionService;
            _actionService.CoreSkill = Context.Role.NextCoreSkills[CellIndex];
        }
    }
}