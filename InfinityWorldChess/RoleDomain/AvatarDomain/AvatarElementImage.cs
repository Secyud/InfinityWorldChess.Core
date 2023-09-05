#region

using Secyud.Ugf.BasicComponents;
using UnityEngine;

#endregion

namespace InfinityWorldChess.RoleDomain
{
    public class AvatarElementImage : SImage
    {
        [SerializeField] private AvatarEditor Editor;
        [SerializeField] private AvatarElementType ElementType;

        private AvatarSpriteContainer _container;

        protected override void Awake()
        {
            Editor.SetImage(this, ElementType);
        }

        public void SetScale(byte scale)
        {
            if (_container is null ) return;

            float value = Mathf.Lerp(
                _container.Message.ScaleMin,
                _container.Message.ScaleMax,
                GetRange01(scale)) * Editor.Scale / 768;

            rectTransform.localScale = new Vector2(value, value);
        }

        public void SetPositionX(byte positionX)
        {
            if (_container is null ) return;
            float value =
                Mathf.Lerp(
                    -_container.Message.XRange,
                    _container.Message.XRange,
                    GetRange01(positionX)) +
                _container.Message.BiasX;
            Vector3 position = rectTransform.localPosition;
            position.x = value * Editor.Scale;
            rectTransform.localPosition = position;
        }

        public void SetPositionY(byte positionY)
        {
            if (_container is null ) return;
            float value =
                Mathf.Lerp(
                    -_container.Message.YRange,
                    _container.Message.YRange,
                    GetRange01(255-positionY)) +
                _container.Message.BiasY + Editor.Bias;
            Vector3 position = rectTransform.localPosition;
            position.y = -value * Editor.Scale;
            rectTransform.localPosition = position;
        }

        public void SetRotation(byte rotation)
        {
            if (_container is null ) return;
            float value = Mathf.Lerp(
                _container.Message.RotateMin,
                _container.Message.RotateMax,
                GetRange01(rotation));
            rectTransform.rotation = Quaternion.Euler(0, 0, value);
        }

        public void SetSprite(AvatarSpriteContainer container)
        {
            _container = container;
            Sprite = container?.Sprite;
            if (Sprite)
            {
                SetNativeSize();
            }
        }


        protected float GetRange01(int value)
        {
            return value / 255f;
        }
    }
}