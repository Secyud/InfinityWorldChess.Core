using System.Collections.Generic;
using System.IO;
using InfinityWorldChess.BundleDomain;
using InfinityWorldChess.PlayerDomain;
using Secyud.Ugf;
using Secyud.Ugf.Archiving;
using Secyud.Ugf.DataManager;
using UnityEngine;

namespace InfinityWorldChess.GameDomain.WorldMapDomain
{
    public sealed class WorldSetting : IShowable,IDataResource
    {
        [field: S(2)] public string Description { get; set; }
        [field: S(0)] public string Name { get; set; }

        [field: S(0)] public string ResourceId { get; set; }
        [field: S(1)] public int PositionX { get; set; }
        [field: S(1)] public int PositionZ { get; set; }
        [field: S(32)] public IObjectAccessor<Sprite> Icon { get; set; }
        [field: S(31)] public List<IBundle> PlayBundles { get; } = new();

        public void PreparePlayer(PlayerGameContext context)
        {
            context.Role.Position = GameScope.Instance.GetCellR(PositionX,PositionZ);
        }

        public string GetDataDirectory(string fileName)
        {
            return Path.Combine(U.Path,"Data/Play",Name,fileName);
        }
    }
}