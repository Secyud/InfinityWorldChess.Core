#region

using System.Collections.Generic;
using InfinityWorldChess.SkillDomain;
using Secyud.Ugf.HexMap;
using System.Linq;
using UnityEngine;

#endregion

namespace InfinityWorldChess.BattleDomain
{
    public partial class BattleContext
    {
        private RoleBattleChess _currentRole;
        private SkillContainer _currentSkill;
        private BattleChecker _hoverChecker;
        private BattleChecker _selectedChecker;
        private IBattleChess _selectedChess;
        private readonly List<BattleChecker> _skillPositionCheckers = new();
        private readonly List<BattleChecker> _skillResultCheckers = new();

        public RoleBattleChess CurrentRole
        {
            get => _currentRole;
            private set
            {
                _currentRole = value;
                CurrentSkill = GetSkillAvailable();
                Ui.BattleRoleActiveMessage.OnInitialize(value);
            }
        }

        public SkillContainer CurrentSkill
        {
            get => _currentSkill;
            set
            {
                _currentSkill = value;
                if (_skillPositionCheckers.Any())
                {
                    foreach (BattleChecker checker in _skillPositionCheckers)
                        checker.Releasable = false;

                    _skillPositionCheckers.Clear();
                }

                if (value is null) return;

                ISkillRange range = value.Skill
                    .GetCastPositionRange(_currentRole);
                foreach (HexCell area in range.Value)
                {
                    BattleChecker checker = GetChecker(area);
                    if (checker is null) continue;

                    checker.Releasable = true;
                    _skillPositionCheckers.Add(checker);
                }
            }
        }

        public BattleChecker HoverChecker
        {
            get => _hoverChecker;
            private set
            {
                if (_hoverChecker == value) return;

                _hoverChecker?.SetHighlight();
                _hoverChecker = value;
                _hoverChecker?.Cell.EnableHighlight(Color.white);
            }
        }

        public void SetHoverRange(BattleChecker c)
        {
            if (_skillResultCheckers.Any())
            {
                foreach (BattleChecker checker in _skillResultCheckers)
                    checker.InRange = false;

                _skillResultCheckers.Clear();
            }

            if (c is null || !_skillPositionCheckers.Contains(c)) return;

            ISkillRange range = CurrentSkill.Skill
                .GetCastResultRange(_currentRole, c.Cell);
            
            foreach (HexCell area in range.Value)
            {
                BattleChecker checker = GetChecker(area);
                if (checker is null) continue;

                checker.InRange = true;
                _skillResultCheckers.Add(checker);
            }
        }

        public BattleChecker SelectedChecker
        {
            get => _selectedChecker;
            private set
            {
                if (_selectedChecker is not null)
                    _selectedChecker.Selected = false;
                _selectedChecker = value;
                if (_selectedChecker is not null)
                    _selectedChecker.Selected = true;

                Ui.BattleCheckerMessage.OnInitialize(value);
                IBattleChess chess = GetChess(value?.Cell ? value.Cell.Unit : null);
            }
        }

        public IBattleChess SelectedChess
        {
            get => _selectedChess;
            set
            {
                _selectedChess = value;
                
                
                Ui.BattleChessSelectMessage.OnInitialize(value);
            }
        }

        private SkillContainer GetSkillAvailable()
        {
            RoleBattleChess role = CurrentRole;

            if (role is null) return null;

            CoreSkillContainer[] coreSkills = role.NextCoreSkills;
            FormSkillContainer[] formSkills = role.NextFormSkills;

            SkillContainer skill = CurrentSkill;
            if (skill is not null && skill.Skill.CheckCastCondition(role) is null)
                if (coreSkills.Any(u => u == CurrentSkill) ||
                    formSkills.Any(u => u == CurrentSkill))
                    return CurrentSkill;

            for (int i = 0; i < SharedConsts.CoreSkillCodeCount; i++)
                if (coreSkills[i] is not null &&
                    coreSkills[i].CoreSkill.CheckCastCondition(role) is null)
                    return coreSkills[i];

            for (int i = 0; i < SharedConsts.FormSkillTypeCount; i++)
                if (formSkills[i] is not null &&
                    formSkills[i].FormSkill.CheckCastCondition(role) is null)
                    return formSkills[i];

            return null;
        }
    }
}