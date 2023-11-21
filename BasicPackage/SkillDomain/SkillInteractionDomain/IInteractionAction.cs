using Secyud.Ugf;

namespace InfinityWorldChess.SkillDomain
{
    public interface IInteractionAction:IHasContent,IActiveSkillAttached
    {
        void Invoke(SkillInteraction interaction);
    }
}