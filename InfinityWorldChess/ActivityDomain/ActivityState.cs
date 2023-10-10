using System;

namespace InfinityWorldChess.ActivityDomain
{
    [Serializable]
    public enum ActivityState
    {
        NotReceived,
        Failed,
        Received,
        Success,
    }
}