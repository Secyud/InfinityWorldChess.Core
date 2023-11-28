using Secyud.Ugf;
using Secyud.Ugf.BasicComponents;
using Secyud.Ugf.Collections;
using Secyud.Ugf.TableComponents;
using UnityEngine;

namespace InfinityWorldChess.RoleDomain
{
    [RequireComponent(typeof(RectTransform))]
    public sealed class AvatarEditor : TableCell
    {
        [SerializeField] private SText RoleName;
        [SerializeField] private float Size;
        [SerializeField] private float BiasY;

        [SerializeField] private AvatarElementImage[] Images;

        public RegistrableDictionary<int, AvatarSpriteContainer>[] Group =>
            _female ? Manager.FemaleAvatarResource : Manager.MaleAvatarResource;

        public AvatarElementImage[] Elements => Images;

        private bool _female;

        private RoleResourceManager _manager;
        private RoleResourceManager Manager => _manager ??= U.Get<RoleResourceManager>();

        public float Scale => Size;
        public float Bias => BiasY;

        private void Awake()
        {
            GetComponent<RectTransform>();
            // Size = Content.rect.width * 0.65f;
            // Content.pivot = Content.anchorMin = Content.anchorMax = new Vector2(0.5f, 0.5f);
            // Content.anchoredPosition = Vector2.zero;
            // Content.sizeDelta = new Vector2(Size, Size * 1.5f);
            // Size /= 120;
        }

        private void Bind(Role.BasicProperty basic)
        {
            _female = basic.Female;

            for (int i = 0; i < IWCC.AvatarElementCount; i++)
            {
                AvatarElementImage image = Images[i];
                
                if (!image)
                    continue;
                
                AvatarElement avatar = basic.Avatar[i];
                AvatarSpriteContainer container = Group[i].Get(avatar.Id);

                image.SetSprite(container);
                image.SetScale(avatar.Scale);
                image.SetPositionX(avatar.PositionX);
                image.SetPositionY(avatar.PositionY);
                image.SetRotation(avatar.Rotation);
                container?.SetImage(image);
            }

            if (RoleName)
                RoleName.text = basic.Name;
        }

        public void SetImage(AvatarElementImage image, AvatarElementType type)
        {
            Images[(int)type] = image;
        }

        public void OnInitialize(Role.BasicProperty basic)
        {
            if (basic is null)
                Clear();
            else
                Bind(basic);
        }

        private void Clear()
        {
            foreach (AvatarElementImage image in Images)
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
            return Group[(int)elementType].GetByIndex(index);
        }
    }
}