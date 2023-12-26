using System.Runtime.InteropServices;
using Secyud.Ugf.AssetComponents;
using Secyud.Ugf.DataManager;
using UnityEngine;

namespace InfinityWorldChess.GlobalDomain
{
    [Guid("FD7A208F-8A23-3FE1-F973-368E80804AF0")]
    public class IconContainer:SpriteContainer
    {
        protected override Sprite GetObject()
        {
            return Loader.LoadAsset<Sprite>($"Icons/{AssetName}.png");
        }
    }
}