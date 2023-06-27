#region

using InfinityWorldChess.Ugf;
using Secyud.Ugf.AssetLoading;
using Secyud.Ugf.TableComponents;
using System.Collections.Generic;
using Secyud.Ugf.DependencyInjection;
using UnityEngine.Events;

#endregion

namespace InfinityWorldChess.SkillDomain
{
	[Registry()]
	public class SkillGameContext 
	{
		public readonly IMonoContainer<SelectableTable> CoreSkillSelectTable;
		public readonly IMonoContainer<SelectableTable> FormSkillSelectTable;
		public readonly IMonoContainer<SelectableTable> PassiveSkillSelectTable;

		private IwcTableHelperSh<ICoreSkill, CoreSkillTf> _coreSkillTableHelper;
		private IwcTableHelperSh<IFormSkill, FormSkillTf> _formSkillTableHelper;
		private IwcTableHelperSh<IPassiveSkill, PassiveSkillTf> _passiveSkillTableHelper;

		public SkillGameContext(IwcAb ab)
		{
			CoreSkillSelectTable = MonoContainer<SelectableTable>.Create(ab);
			FormSkillSelectTable = MonoContainer<SelectableTable>.Create(ab);
			PassiveSkillSelectTable = MonoContainer<SelectableTable>.Create(ab);
		}

		public virtual void OnCoreSkillSelectionOpen(List<ICoreSkill> items, UnityAction<ICoreSkill> action)
		{
			CoreSkillSelectTable.Create();
			_coreSkillTableHelper = new IwcTableHelperSh<ICoreSkill, CoreSkillTf>
			{
				CallBackAction = action
			};
			_coreSkillTableHelper.OnInitialize(
				CoreSkillSelectTable.Value, IwcAb.Instance.VerticalCellInk.Value, items
			);
			CoreSkillSelectTable.Value.CancelAction += OnCoreSkillSelectionClose;
		}

		public virtual void OnCoreSkillSelectionClose()
		{
			CoreSkillSelectTable.Destroy();
			_coreSkillTableHelper = null;
		}

		public virtual void OnFormSkillSelectionOpen(List<IFormSkill> items, UnityAction<IFormSkill> action)
		{
			FormSkillSelectTable.Create();
			_formSkillTableHelper = new IwcTableHelperSh<IFormSkill, FormSkillTf>
			{
				CallBackAction = action
			};
			_formSkillTableHelper.OnInitialize(
				FormSkillSelectTable.Value, IwcAb.Instance.VerticalCellInk.Value, items
			);
			FormSkillSelectTable.Value.CancelAction += OnFormSkillSelectionClose;
		}

		public virtual void OnFormSkillSelectionClose()
		{
			FormSkillSelectTable.Destroy();
			_formSkillTableHelper = null;
		}

		public virtual void OnPassiveSkillSelectionOpen(List<IPassiveSkill> items,
			UnityAction<IPassiveSkill> action)
		{
			PassiveSkillSelectTable.Create();
			_passiveSkillTableHelper = new IwcTableHelperSh<IPassiveSkill, PassiveSkillTf>
			{
				CallBackAction = action
			};
			_passiveSkillTableHelper.OnInitialize(
				PassiveSkillSelectTable.Value, IwcAb.Instance.VerticalCellInk.Value, items
			);
			PassiveSkillSelectTable.Value.CancelAction += OnPassiveSkillSelectionClose;
		}

		public virtual void OnPassiveSkillSelectionClose()
		{
			PassiveSkillSelectTable.Destroy();
			_passiveSkillTableHelper = null;
		}
	}
}