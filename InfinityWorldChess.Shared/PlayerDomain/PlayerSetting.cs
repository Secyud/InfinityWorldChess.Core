#region

using Secyud.Ugf.Archiving;
using System.IO;
using Secyud.Ugf;
using Secyud.Ugf.DataManager;

#endregion

namespace InfinityWorldChess.PlayerDomain
{
    public class PlayerSetting : IArchivable
    {
        [S(0)] public bool DongChaRenXin = true; //     洞察人心

        [S(1)] public bool QiaoDuoTianGong = true; //     巧夺天工

        [S(2)] public bool WuXueQiCai = true; //     武学奇才

        [S(3)] public bool YunChouWeiWo = true; //     运筹帷幄


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