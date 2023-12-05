using InfinityWorldChess.BuffDomain;
using InfinityWorldChess.ItemDomain;
using Secyud.Ugf;

namespace InfinityWorldChess.SkillDomain
{
    public interface ISkill:IShowable,IHasScore,IAttachProperty
    {
        IObjectAccessor<SkillAnimBase> UnitPlay { get; set; }
    }
}