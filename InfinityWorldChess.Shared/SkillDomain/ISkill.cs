using InfinityWorldChess.GlobalDomain;
using Secyud.Ugf;
using Secyud.Ugf.HexMap;

namespace InfinityWorldChess.SkillDomain
{
    public interface ISkill:IHasSaveIndex,IShowable
    {
        byte Score { get; set; }
		
        IObjectAccessor<HexUnitPlay> UnitPlay { get; set; }
    }
}