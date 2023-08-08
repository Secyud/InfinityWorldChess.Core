using Secyud.Ugf;
using Secyud.Ugf.BasicComponents;
using Secyud.Ugf.Collections;
using Secyud.Ugf.TableComponents;
using UnityEngine;

namespace InfinityWorldChess.RoleDomain
{
    [RequireComponent(typeof(RectTransform))]
    public class AvatarEditor : TableCell
    {
        [SerializeField] private SText RoleName;
        [SerializeField] private float Size;
        [SerializeField] private float BiasY;

        private AvatarElementImage[] _images;
        private AvatarElementSlider[] _sliders;
        private Role.BasicProperty _basic;

        private RegistrableDictionary<int, AvatarSpriteContainer>[] _group;
        private RoleResourceManager _manager;
        private RectTransform _content;

        public float Scale => Size;
        public float Bias => BiasY;

        protected virtual void Awake()
        {
            _manager = U.Get<RoleResourceManager>();
            _images ??= new AvatarElementImage[SharedConsts.AvatarElementCount];
            _content = GetComponent<RectTransform>();
            // Size = Content.rect.width * 0.65f;
            // Content.pivot = Content.anchorMin = Content.anchorMax = new Vector2(0.5f, 0.5f);
            // Content.anchoredPosition = Vector2.zero;
            // Content.sizeDelta = new Vector2(Size, Size * 1.5f);
            // Size /= 120;
        }

        public void Bind(Role.BasicProperty basic)
        {
            _group = basic.Female ? _manager.FemaleAvatarResource : _manager.MaleAvatarResource;

            if (_sliders is not null)
            {
                for (int i = 0; i < SharedConsts.AvatarElementCount; i++)
                {
                    if (!_sliders[i]) continue;
                    int max = _group[i].KeyList.Count;
                    if (max > 0) max -= 1;
                    _sliders[i].SetMaxValue(max);
                }
            }

            for (int i = 0; i < SharedConsts.AvatarElementCount; i++)
            {
                AvatarElementImage image = _images[i];
                AvatarElement avatar = _basic.Avatar[i];
                AvatarSpriteContainer container = _group[i].Get(avatar.Id);

                image.SetSprite(container);
                image.SetScale(avatar.Scale);
                image.SetPositionX(avatar.PositionX);
                image.SetPositionY(avatar.PositionY);
                image.SetRotation(avatar.Rotation);
                container.SetImage(image);
            }

            if (RoleName)
                RoleName.text = basic.Name;
        }

        public void SetImage(AvatarElementImage image, AvatarElementType type)
        {
            _images[(int)type] = image;
        }

        public void SetSlider(AvatarElementSlider slider, AvatarElementType type)
        {
            _sliders ??= new AvatarElementSlider[SharedConsts.AvatarElementCount];

            if (slider is not null)
            {
                slider.Image = _images[(int)type];
                if (slider.Type == AvatarSliderType.Sprite)
                    _sliders[(int)type] = slider;
            }
        }

        public virtual void OnInitialize(Role.BasicProperty basic)
        {
            if (basic is null)
                Clear();
            else
                Bind(basic);
        }

        public virtual void Clear()
        {
            foreach (AvatarElementImage image in _images)
            {
                if (image)
                    image.SetSprite(null);
            }

            if (RoleName)
                RoleName.text = "";
        }


        public static void SetCell(TableCell cell, Role role)
        {
            ((AvatarEditor)cell).OnInitialize(role?.Basic);
        }

        public AvatarSpriteContainer GetElement(int index, AvatarElementType elementType)
        {
            return _group[(int)elementType].GetByIndex(index);
        }
    }
}