using System;

namespace InfinityWorldChess.ActivityDomain
{
    [Serializable]
    public enum ActivityState
    {
        NotReceived = 0,
        Failed = 1,
        Received = 2,
        Success = 3,
    }
}