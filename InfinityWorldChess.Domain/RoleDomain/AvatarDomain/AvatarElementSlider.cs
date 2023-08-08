using Secyud.Ugf.BasicComponents;
using UnityEngine;

namespace InfinityWorldChess.RoleDomain
{
    [RequireComponent(typeof(SSlider))]
    public class AvatarElementSlider : MonoBehaviour
    {
        [SerializeField] private AvatarEditor Editor;
        [SerializeField] private AvatarElementType ElementType;
        [SerializeField] private AvatarSliderType SliderType;

        private SSlider _slider;

        public AvatarElementImage Image { get; set; }
        public AvatarSliderType Type => SliderType;

        private void Awake()
        {
            Editor.SetSlider(this, ElementType);
            _slider = GetComponent<SSlider>();
        }

        public void SetMaxValue(int max)
        {
            _slider.maxValue = max;
            _slider.minValue = 0;
        }

        public void SetScale(float scale)
        {
            if (Image)
                Image.SetScale((byte)scale);
        }

        public void SetPositionX(float positionX)
        {
            if (Image)
                Image.SetPositionX((byte)positionX);
        }

        public void SetPositionY(float positionX)
        {
            if (Image)
                Image.SetPositionY((byte)positionX);
        }

        public void SetRotation(float rotation)
        {
            if (Image)
                Image.SetRotation((byte)rotation);
        }

        public void SetSprite(float index)
        {
            if (Image)
                Image.SetSprite(Editor.GetElement((int)index,ElementType));
        }
    }
}