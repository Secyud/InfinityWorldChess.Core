using System;

namespace InfinityWorldChess.FunctionDomain
{
    public interface ITriggerable
    {
        event Action ExtraActions;
    }
}