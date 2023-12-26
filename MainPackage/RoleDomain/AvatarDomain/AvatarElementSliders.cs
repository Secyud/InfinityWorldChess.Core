using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace InfinityWorldChess.RoleDomain
{
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