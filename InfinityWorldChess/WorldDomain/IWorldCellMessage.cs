using UnityEngine;

namespace InfinityWorldChess.WorldDomain
{
    public interface IWorldCellMessage
    {
        int Index { get; }

        Transform GetModelPrefab();
    }
}