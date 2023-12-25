using System.Runtime.InteropServices;
using Secyud.Ugf.DataManager;

namespace InfinityWorldChess.SkillDomain
{
    [Guid("B85B9635-E7EF-5721-FB81-E0644507302E")]
    public class StartEndRange 
    {
        [field: S(-1)] public byte Start { get; set; }
        [field: S(-1)] public byte End { get; set; }

        protected virtual string SeLabel => $"({Start},{End})";

        protected virtual string Description => null;
    }
}