using Secyud.Ugf;

namespace InfinityWorldChess.SkillDomain
{
    public interface IInteractionAction:IHasContent,ISkillAttached
    {
        void Invoke(SkillInteraction interaction);
    }
}