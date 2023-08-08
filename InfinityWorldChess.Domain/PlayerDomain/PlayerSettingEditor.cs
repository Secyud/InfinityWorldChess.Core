#region

using Secyud.Ugf.EditorComponents;

#endregion

namespace InfinityWorldChess.PlayerDomain
{
    public class PlayerSettingEditor : EditorBase<PlayerSetting>
    {
        public void SetWuXueQiCai(bool b)
        {
            Property.WuXueQiCai = b;
        }

        public void SetQiaoDuoTianGong(bool b)
        {
            Property.QiaoDuoTianGong = b;
        }

        public void SetDongChaRenXin(bool b)
        {
            Property.DongChaRenXin = b;
        }

        public void SetYunChouWeiWo(bool b)
        {
            Property.YunChouWeiWo = b;
        }
    }
}