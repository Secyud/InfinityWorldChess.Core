#region

using InfinityWorldChess.GameDomain;
using Secyud.Ugf.LayoutComponents;
using UnityEngine;

#endregion

namespace InfinityWorldChess.PlayerDomain
{
	public class PlayerSettingController : MonoBehaviour
	{
		[SerializeField] private int Type;

		private void Awake()
		{
			PlayerSetting setting =  GameScope.Instance.Player.PlayerSetting;
			if (setting is null)
				enabled = false;
			else if (Type switch
			{
				0 => setting.DongChaRenXin,
				1 => setting.QiaoDuoTianGong,
				2 => setting.WuXueQiCai,
				3 => setting.YunChouWeiWo,
				_ => true
			})
				enabled = false;
			else
				gameObject.SetActive(false);
			if (transform.parent.TryGetComponent(out LayoutGroupTrigger trigger))
				trigger.enabled = true;
		}
	}
}