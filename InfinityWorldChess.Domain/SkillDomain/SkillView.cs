#region

using InfinityWorldChess.RoleDomain;
using InfinityWorldChess.Ugf;
using Secyud.Ugf.TableComponents;
using System.Diagnostics.CodeAnalysis;
using InfinityWorldChess.PlayerDomain;
using Secyud.Ugf;
using UnityEngine;

#endregion

namespace InfinityWorldChess.SkillDomain
{
	public class SkillView : MonoBehaviour
	{

		[SerializeField] private FunctionalTable Table;

		private Role _role;
		private GameScope _scope;

		private void Awake()
		{
			_scope = U.Factory.Application.DependencyManager.GetScope<GameScope>();
		}

		public void ChangeView(int type)
		{
			if (_role is null)
				return;

			switch (type)
			{
			case 0:
			{
				IwcTableHelperBh<ICoreSkill, CoreSkillTf,CoreSkillBf> helper = 
					new(GameScope.RefreshRoleMessageMenu);
				helper.OnInitialize(
					Table,
					IwcAb.Instance.VerticalCellInk.Value,
					_role.CoreSkill.LearnedSkills
				);
				break;
			}
			case 1:
			{
				IwcTableHelperBh<IFormSkill, FormSkillTf,FormSkillBf> helper =
					new(GameScope.RefreshRoleMessageMenu);
				helper.OnInitialize(
					Table,
					IwcAb.Instance.VerticalCellInk.Value,
					_role.FormSkill.LearnedSkills
				);
				break;
			}
			case 2:
			{
				IwcTableHelperBh<IPassiveSkill, PassiveSkillTf,PassiveSkillBf> helper =
					new(GameScope.RefreshRoleMessageMenu);
				helper.OnInitialize(
					Table,
					IwcAb.Instance.VerticalCellInk.Value,
					_role.PassiveSkill.LearnedSkills
				);
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
			Table.RefreshFilter();
		}
	}
}