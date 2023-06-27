﻿using InfinityWorldChess.RoleDomain;
using InfinityWorldChess.Ugf;
using Secyud.Ugf.BasicComponents;
using Secyud.Ugf.Layout;
using UnityEngine;

namespace InfinityWorldChess.BattleDomain
{
	public class BattleChessMessage : MonoBehaviour
	{
		[SerializeField] private LayoutGroupTrigger BuffContent;
		[SerializeField] private ImageCell SmallImageCellPrefab;
		[SerializeField] private SSlider Health;
		[SerializeField] private SText HealthText;
		[SerializeField] private SSlider Energy;
		[SerializeField] private SText EnergyText;
		[SerializeField] private SSlider Execution;
		[SerializeField] private SText ExecutionText;
		[SerializeField] private RoleAvatarCell Avatar;


		private void ClearBuffContent()
		{
			Transform buffTrans = BuffContent.transform;
			for (int i = 0; i < buffTrans.childCount; i++)
				Destroy(buffTrans.GetChild(i));
		}

		public void SetHealth(RoleBattleChess role)
		{
			SetSliderAndText(Health, HealthText, role.HealthValue, role.MaxHealthValue);
		}
		public void SetEnergy(RoleBattleChess role)
		{
			SetSliderAndText(Energy, EnergyText, role.EnergyValue, role.MaxEnergyValue);
		}
		public void SetExecution(RoleBattleChess role)
		{
			SetSliderAndText(Execution, ExecutionText, role.ExecutionValue, SharedConsts.MaxExecutionValue);
		}

		private static void SetSliderAndText(SSlider slider, SText text,float value,float max)
		{
			slider.maxValue = max;
			slider.value = value;
			text.text = $"{value:F0}/{max:F0}";
		}

		public void OnInitialize(IBattleChess chess = null)
		{
			if (chess == null)
				gameObject.SetActive(false);
			else
			{
				gameObject.SetActive(true);
				ClearBuffContent();
				RectTransform buffTrans =BuffContent.PrepareLayout();

				
				// Hoverable hoverable = Avatar.gameObject.GetOrAddComponent<Hoverable>();
				// hoverable.Bind(
				// 	() =>
				// 	{
				// 		role.CreateFloating();
				// 	});
			}
		}
	}
}