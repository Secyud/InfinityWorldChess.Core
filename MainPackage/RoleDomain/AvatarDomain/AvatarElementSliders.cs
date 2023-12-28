using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace InfinityWorldChess.RoleDomain
{
    /// <summary>
    /// 滑动条汇总，用于管理所有捏脸元素的滑动条
    /// </summary>
    public class AvatarElementSliders : MonoBehaviour
    {
        [SerializeField] private AvatarEditor AvatarEditor;

        public List<AvatarElementSlider> Sliders { get; } = new();

        public AvatarEditor Editor => AvatarEditor;

        public void RefreshMaxValue()
        {
            foreach (AvatarElementSlider slider in Sliders.Where(slider => slider))
            {
                slider.SetMaxValue();
            }
        }
    }
}