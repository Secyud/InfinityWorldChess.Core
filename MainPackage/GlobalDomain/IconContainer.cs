using System.Runtime.InteropServices;
using Secyud.Ugf.AssetComponents;
using UnityEngine;

namespace InfinityWorldChess.GlobalDomain
{
    /// <summary>
    /// 一个简单的归纳，把icon目录单独提出来
    /// </summary>
    [Guid("FD7A208F-8A23-3FE1-F973-368E80804AF0")]
    public class IconContainer:SpriteContainer
    {
        protected override Sprite GetObject()
        {
            return Loader.LoadAsset<Sprite>($"Icons/{AssetName}.png");
        }
    }
}