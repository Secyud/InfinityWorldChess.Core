#region

using InfinityWorldChess.GlobalDomain;
using Secyud.Ugf;

#endregion

namespace InfinityWorldChess.SkillDomain
{
	public interface IPassiveSkill : IShowable, ICanBeEquipped, IHasContent, ISkill
	{
		// 阴阳
		public short Yin { get; set; }

		public short Yang { get; set; }

		// 生杀灵御
		public byte Living { get; set; }

		public byte Kiling { get; set; }

		public byte Nimble { get; set; }

		public byte Defend { get; set; }
	}
}