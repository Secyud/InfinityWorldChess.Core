using Secyud.Ugf.DataManager;

namespace InfinityWorldChess.SkillDomain
{
    public class StartEndRange 
    {
        [field: S(-1)] public byte Start { get; set; }
        [field: S(-1)] public byte End { get; set; }

        protected virtual string SeLabel => $"({Start},{End})";

        protected virtual string Description => null;
    }
}