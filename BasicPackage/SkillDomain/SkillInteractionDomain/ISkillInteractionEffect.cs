using Secyud.Ugf;

namespace InfinityWorldChess.SkillDomain
{
    public interface ISkillInteractionEffect:IHasContent,IActiveSkillAttached
    {
        void Invoke(SkillInteraction interaction);
    }
}