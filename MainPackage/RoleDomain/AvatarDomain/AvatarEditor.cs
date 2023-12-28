using System.Collections.Generic;
using Secyud.Ugf;
using Secyud.Ugf.BasicComponents;
using Secyud.Ugf.Collections;
using Secyud.Ugf.TableComponents;
using UnityEngine;

namespace InfinityWorldChess.RoleDomain
{
    /// <summary>
    /// 汇总个体组件，这个组件可以设置整体大小和偏移用于调试，同时提供外部接口。
    /// </summary>
    [RequireComponent(typeof(RectTransform))]
    public sealed class AvatarEditor : TableCell
    {
        [SerializeField] private SText RoleName;
        [SerializeField] private float Size;
        [SerializeField] private float BiasY;
        [SerializeField] private AvatarElementImage[] Images;

        private bool _female;
        private RoleResourceManager _manager;
        
        public RegistrableDictionary<int, AvatarSpriteContainer>[] Group =>
            _female ? _manager.FemaleAvatarResource : _manager.MaleAvatarResource;
        public IReadOnlyList<AvatarElementImage> Elements => Images;
        public float Scale => Size;
        public float Bias => BiasY;

        private void Awake()
        {
            _manager = U.Get<RoleResourceManager>();
        }

        private void Bind(Role.BasicProperty basic)
        {
            _female = basic.Female;

            for (int i = 0; i < MainPackageConsts.AvatarElementCount; i++)
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
            }

            if (RoleName)
            {
                RoleName.text = basic.Name;
            }
        }

        public void SetImage(AvatarElementImage image, AvatarElementType type)
        {
            Images[(int)type] = image;
        }

        public void OnInitialize(Role.BasicProperty basic)
        {
            if (basic is null)
            {
                Clear();
            }
            else
            {
                Bind(basic);
            }
        }

        private void Clear()
        {
            foreach (AvatarElementImage image in Images)
            {
                if (image)
                    image.SetSprite(null);
            }

            if (RoleName)
            {
                RoleName.text = "";
            }
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