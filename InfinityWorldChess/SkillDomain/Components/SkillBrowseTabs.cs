#region

using System.Diagnostics.CodeAnalysis;
using InfinityWorldChess.RoleDomain;
using InfinityWorldChess.Ugf;
using Secyud.Ugf.TableComponents;
using UnityEngine;

#endregion

namespace InfinityWorldChess.SkillDomain
{
	public class SkillBrowseTabs : MonoBehaviour
	{
		[SerializeField] private Table SkillTable;

		private Role _role;

		public void ChangeView(int type)
		{
			if (_role is null)
				return;

			SkillTable.Clear();
			switch (type)
			{
			case 0:
			{
				SkillTable.AutoSetButtonTable
					<ICoreSkill, CoreSkillSorters,CoreSkillFilters,CoreSkillButtons>(
						_role.CoreSkill.LearnedSkills);
				break;
			}
			case 1:
			{
				SkillTable.AutoSetButtonTable
					<IFormSkill, FormSkillSorters,FormSkillFilters,FormSkillButtons>(
						_role.FormSkill.LearnedSkills);
				break;
			}
			case 2:
			{
				SkillTable.AutoSetButtonTable
					<IPassiveSkill, PassiveSkillSorters,PassiveSkillFilters,PassiveSkillButtons>(
						_role.PassiveSkill.LearnedSkills);
				break;
			}
			}
		}

		public void OnInitialize([NotNull] Role role)
		{
			_role = role;
			ChangeView(0);
		}

		public void Refresh()
		{
			SkillTable.Refresh();
		}
	}
}