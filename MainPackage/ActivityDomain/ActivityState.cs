using System;

namespace InfinityWorldChess.ActivityDomain
{
    [Serializable]
    public enum ActivityState
    {
        NotReceived = 0,
        Received = 1,
        Failed = 2,
        Success = 3,
    }
}