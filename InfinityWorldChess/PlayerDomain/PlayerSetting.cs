#region

using Secyud.Ugf.Archiving;

#endregion

namespace InfinityWorldChess.PlayerDomain
{
    public class PlayerSetting : IArchivable
    {
        // 洞察人心
        public bool DongChaRenXin { get; set; } = true;

        // 巧夺天工
        public bool QiaoDuoTianGong { get; set; } = true;

        // 武学奇才
        public bool WuXueQiCai { get; set; } = true;

        // 运筹帷幄
        public bool YunChouWeiWo { get; set; } = true;


        public void Save(IArchiveWriter writer)
        {
            writer.Write(DongChaRenXin);
            writer.Write(QiaoDuoTianGong);
            writer.Write(WuXueQiCai);
            writer.Write(YunChouWeiWo);
        }

        public void Load(IArchiveReader reader)
        {
            DongChaRenXin = reader.ReadBoolean();
            QiaoDuoTianGong = reader.ReadBoolean();
            WuXueQiCai = reader.ReadBoolean();
            YunChouWeiWo = reader.ReadBoolean();
        }
    }
}