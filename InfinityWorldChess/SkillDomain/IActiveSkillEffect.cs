﻿using InfinityWorldChess.BattleDomain;
using Secyud.Ugf;
using Secyud.Ugf.HexMap;

namespace InfinityWorldChess.SkillDomain
{
    public interface IActiveSkillEffect:IHasDescription
    {
        void Cast(BattleRole role, HexCell releasePosition, ISkillRange range,IActiveSkill skill = null);
    }
}