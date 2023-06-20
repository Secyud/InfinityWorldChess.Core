#region

using Secyud.Ugf;
using Secyud.Ugf.Layout;
using UnityEngine;

#endregion

namespace InfinityWorldChess.GlobalDomain
{
	public class MainMenuComponent : MonoBehaviour
	{
		[SerializeField] private LayoutGroupTrigger GameEnterPanel;
		[SerializeField] private GameSlotCell SlotCellPrefab;
		
		public void OnEnterGameClick(bool value)
		{
			GameEnterPanel.gameObject.SetActive(value);
			GameEnterPanel.enabled = value;
			if (SlotCellPrefab)
			{
				for (int i = 0; i < SharedConsts.SlotsCount; i++)
				{
					GameSlotCell obj = SlotCellPrefab.Instantiate(GameEnterPanel.transform);
					obj.OnInitialize( i);
				}
				SlotCellPrefab = null;
			}
		}


		public void OnSettingsClick()
		{
#if UNITY_EDITOR
			Debug.Log("Settings");
#endif
		}

		public void OnExitGameClick()
		{
			Og.ScopeFactory.GetScope<MainMenuScope>().ExitGame();
		}
	}
}