#region

using Secyud.Ugf.Archiving;
using Secyud.Ugf;
using Secyud.Ugf.DataManager;

#endregion

namespace InfinityWorldChess.PlayerDomain
{
    public class PlayerSetting : IArchivable
    {
        // 洞察人心
        [field: S] public bool DongChaRenXin { get; set; } = true;

        // 巧夺天工
        [field: S] public bool QiaoDuoTianGong { get; set; } = true;

        // 武学奇才
        [field: S] public bool WuXueQiCai { get; set; } = true;

        // 运筹帷幄
        [field: S] public bool YunChouWeiWo { get; set; } = true;


        public void Save(IArchiveWriter writer)
        {
            U.AutoSaveObject(this, writer);
        }

        public void Load(IArchiveReader reader)
        {
            U.AutoLoadObject(this, reader);
        }
    }
}