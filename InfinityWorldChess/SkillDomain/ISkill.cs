using InfinityWorldChess.GlobalDomain;
using Secyud.Ugf;
using Secyud.Ugf.HexMap;

namespace InfinityWorldChess.SkillDomain
{
    public interface ISkill:IShowable
    {
        byte Score { get; set; }
		
        IObjectAccessor<SkillPlay> UnitPlay { get; set; }
        
        // 生杀灵御
        public byte Living { get; set; }

        public byte Kiling { get; set; }

        public byte Nimble { get; set; }

        public byte Defend { get; set; }
    }
}