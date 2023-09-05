using System;
using System.Text.RegularExpressions;
using System.Ugf.Collections.Generic;
using Secyud.Ugf.BasicComponents;
using Secyud.Ugf.Collections;
using UnityEngine;

namespace InfinityWorldChess.RoleDomain
{
    public class AvatarElementSlider : MonoBehaviour
    {
        [SerializeField] private AvatarElementSliders Sliders;
        [SerializeField] private AvatarElementType[] ElementType;
        [SerializeField] private AvatarSliderType SliderType;

        private AvatarEditor Editor => Sliders.Editor;
        
        private void Awake()
        {
            if (SliderType == AvatarSliderType.Sprite)
            {
                Sliders.Sliders.AddIfNotContains(this);
                SetMaxValue();
            }
        }

        public void SetMaxValue()
        {
            RegistrableDictionary<int, AvatarSpriteContainer> group = Editor.Group[(int)ElementType[0]];

            SSlider slider = GetComponent<SSlider>();
            slider.maxValue = Math.Max(0,group.KeyList.Count-1) ;
            slider.minValue = 0;
        }

        public void SetScale(float scale)
        {
            foreach (AvatarElementType type in ElementType)
            {
                AvatarElementImage image = Editor.Elements[(int)type];
                if (image)
                    image.SetScale((byte)scale);
            }
        }

        public void SetPositionX(float positionX)
        {
            foreach (AvatarElementType type in ElementType)
            {
                AvatarElementImage image = Editor.Elements[(int)type];
                if (image)
                    image.SetPositionX((byte)positionX);
            }
        }

        public void SetPositionY(float positionY)
        {
            foreach (AvatarElementType type in ElementType)
            {
                AvatarElementImage image = Editor.Elements[(int)type];
                if (image)
                    image.SetPositionY((byte)positionY);
            }
        }

        public void SetPositionXY(Vector2 position)
        {
            foreach (AvatarElementType type in ElementType)
            {
                AvatarElementImage image = Editor.Elements[(int)type];
                if (image)
                {
                    image.SetPositionX((byte)position.x);
                    image.SetPositionY((byte)position.y);
                }
            }
        }

        public void SetRotation(float rotation)
        {
            foreach (AvatarElementType type in ElementType)
            {
                AvatarElementImage image = Editor.Elements[(int)type];
                if (image)
                {
                    image.SetRotation((byte)rotation);
                }
            }
        }

        public void SetSprite(float index)
        {
            foreach (AvatarElementType type in ElementType)
            {
                AvatarElementImage image = Editor.Elements[(int)type];
                if (image)
                {
                    image.SetSprite(Editor.GetElement((int)index, type));
                }
            }
        }
    }
}