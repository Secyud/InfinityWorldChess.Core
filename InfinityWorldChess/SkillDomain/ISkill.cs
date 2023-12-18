using InfinityWorldChess.BuffDomain;
using InfinityWorldChess.ItemDomain;
using Secyud.Ugf;
using Secyud.Ugf.AssetComponents;
using Secyud.Ugf.UgfHexMap;

namespace InfinityWorldChess.SkillDomain
{
    public interface ISkill:IShowable,IHasScore,IAttachProperty
    {
        PrefabContainer<UgfUnitEffect> UnitPlay { get; set; }
    }
}