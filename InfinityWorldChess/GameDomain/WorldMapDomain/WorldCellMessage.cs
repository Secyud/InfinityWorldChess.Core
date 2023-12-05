using InfinityWorldChess.GameDomain.WorldCellDomain;
using Secyud.Ugf;
using Secyud.Ugf.DataManager;
using UnityEngine;

namespace InfinityWorldChess.GameDomain.WorldMapDomain
{
    public class WorldCellMessage : IHasName
    {
        [field: S] public int Id { get; set; }
        [field: S] public int PositionX { get; set; }
        [field: S] public int PositionZ { get; set; }
        [field: S] public IObjectAccessor<Transform> FeaturePrefab { get; set; }
        [field: S] public string Name { get; set; }
        public WorldCell Cell => GameScope.Instance.GetCellR(PositionX,PositionZ);
        
    }
}