#region

using Secyud.Ugf.Archiving;
using System.IO;

#endregion

namespace InfinityWorldChess.PlayerDomain
{
	public class PlayerSetting : IArchivable
	{
		public bool DongChaRenXin = true; //     洞察人心

		public bool QiaoDuoTianGong = true; //     巧夺天工

		public bool WuXueQiCai = true; //     武学奇才

		public bool YunChouWeiWo = true; //     运筹帷幄

		public void Save(BinaryWriter writer)
		{
			writer.Write(DongChaRenXin);
			writer.Write(QiaoDuoTianGong);
			writer.Write(WuXueQiCai);
			writer.Write(YunChouWeiWo);
		}

		public void Load(BinaryReader reader)
		{
			DongChaRenXin = reader.ReadBoolean();
			QiaoDuoTianGong = reader.ReadBoolean();
			WuXueQiCai = reader.ReadBoolean();
			YunChouWeiWo = reader.ReadBoolean();
		}
	}
}