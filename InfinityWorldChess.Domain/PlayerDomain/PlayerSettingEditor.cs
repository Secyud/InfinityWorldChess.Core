#region

using UnityEngine;

#endregion

namespace InfinityWorldChess.PlayerDomain
{
	public class PlayerSettingEditor : MonoBehaviour
	{
		private PlayerSetting _playerSetting;

		public void SetWuXueQiCai(bool b)
		{
			_playerSetting.WuXueQiCai = b;
		}

		public void SetQiaoDuoTianGong(bool b)
		{
			_playerSetting.QiaoDuoTianGong = b;
		}

		public void SetDongChaRenXin(bool b)
		{
			_playerSetting.DongChaRenXin = b;
		}

		public void SetYunChouWeiWo(bool b)
		{
			_playerSetting.YunChouWeiWo = b;
		}

		public void OnInitialize(PlayerSetting playerSetting)
		{
			_playerSetting = playerSetting;
		}
	}
}