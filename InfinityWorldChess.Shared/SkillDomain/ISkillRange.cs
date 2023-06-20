#region

using Secyud.Ugf.HexMap;

#endregion

namespace InfinityWorldChess.SkillDomain
{
	public interface ISkillRange
	{
		HexCell[] Value { get; }
	}
}