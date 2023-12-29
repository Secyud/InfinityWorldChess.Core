using InfinityWorldChess.BuffDomain;
using InfinityWorldChess.ItemDomain;
using Secyud.Ugf;
using Secyud.Ugf.AssetComponents;
using Secyud.Ugf.UgfHexMap;

namespace InfinityWorldChess.SkillDomain
{
    /// <summary>
    /// 凡是技能，应当拥有基本属性和一个动画，动画可以是粒子动画。
    /// 也可以设置一个Animation附加到unit中。
    /// 被动技能的动画可以设置为空，一般是待机动画。
    /// </summary>
    public interface ISkill:IShowable,IHasScore,IAttachProperty
    {
        PrefabContainer<UgfUnitEffect> UnitPlay { get; set; }
    }
}