using Secyud.Ugf;
using Secyud.Ugf.BasicComponents;
using UnityEngine;

namespace InfinityWorldChess.RoleDomain
{
    public class RoleAvatarCell : MonoBehaviour
    {
        [SerializeField] private AvatarElementImage BackHair;
        [SerializeField] private AvatarElementImage Head;
        [SerializeField] private AvatarElementXySrImage HeadFeature;
        [SerializeField] private AvatarElementSyImage Nose;
        [SerializeField] private AvatarElementSyImage Mouth;
        [SerializeField] private AvatarElementXySrImage LeftEye;
        [SerializeField] private AvatarElementXySrImage RightEye;
        [SerializeField] private AvatarElementXySrImage LeftBrow;
        [SerializeField] private AvatarElementXySrImage RightBrow;
        [SerializeField] private AvatarElementImage Beard;
        [SerializeField] private AvatarElementImage FrontHair;
        [SerializeField] private SText RoleName;
        [SerializeField] protected RectTransform Content;

        protected AvatarResourceGroup Group;
        protected RoleResourceManager Resource;
        protected float Size;
        protected virtual float MultipleSize => Multiple;

        public const float Multiple = 512f / 120;

        protected virtual void Awake()
        {
            Resource = Og.DefaultProvider.Get<RoleResourceManager>();
            Size = Content.rect.width * 0.65f;
            Content.pivot = Content.anchorMin = Content.anchorMax = new Vector2(0.5f, 0.5f);
            Content.anchoredPosition = Vector2.zero;
            Content.sizeDelta = new Vector2(Size, Size * 1.5f);
            Size /= 120;
        }


        public virtual void OnInitialize(Role.BasicProperty basic)
        {
            if (basic is null)
                Clear();
            else
                SetAvatar(basic);
        }

        public void SetBackHair(RoleAvatar.AvatarElement element)
        {
            SetElement(BackHair, Group.BackHair.Get(element.Id));
        }

        public void SetHead(RoleAvatar.AvatarElement element)
        {
            AvatarSpriteContainer2 container = Group.Head.Get(element.Id);
            if (container is null)
            {
                Head.Sprite = null;
                Beard.Sprite = null;
            }
            else
            {
                Head.SetElement(
                    container.First,
                    container.Scale * Size,
                    container.AnchorFirst * MultipleSize);
                container.SetFirstImage(Head);
                Beard.SetElement(
                    container.Second,
                    container.Scale * Size,
                    container.AnchorSecond * MultipleSize);
                container.SetFirstImage(Beard);
            }
        }

        public void SetHeadFeature(RoleAvatar.AvatarElement4 element)
        {
            AvatarSpriteContainer feature = Group.HeadFeature.Get(element.Id);

            if (feature is null)
                HeadFeature.Sprite = null;
            else
            {
                HeadFeature.SetElement(
                    feature.First,
                    feature.AnchorFirst * MultipleSize,
                    feature.Scale * Size,
                    new Vector4(
                        GetRange11(element.X), GetRange11(element.Y),
                        GetRange01(element.Z), GetRange01(element.W)
                    )
                );
                feature.SetFirstImage(HeadFeature);
            }
        }

        public void SetNose(RoleAvatar.AvatarElement2X element)
        {
            SetElement(
                Nose,
                Group.Nose.Get(element.Id1),
                new Vector2(GetRange01(element.X),
                    GetRange11(element.Y))
            );
        }

        public void SetMouth(RoleAvatar.AvatarElement2X element)
        {
            SetElement(
                Mouth,
                Group.Mouth.Get(element.Id2),
                new Vector2(GetRange01(element.Z),
                    GetRange11(element.W))
            );
        }

        public void SetEye(RoleAvatar.AvatarElement4 element)
        {
            SetElement(
                LeftEye,
                RightEye,
                element,
                Group.Eye.Get(element.Id)
            );
        }

        public void SetBrow(RoleAvatar.AvatarElement4 element)
        {
            SetElement(
                LeftBrow,
                RightBrow,
                element,
                Group.Brow.Get(element.Id)
            );
        }


        public void SetFrontHair(RoleAvatar.AvatarElement element)
        {
            SetElement(
                FrontHair,
                Group.FrontHair.Get(element.Id));
        }

        protected void SetElement(AvatarElementImage image, AvatarSpriteContainer container)
        {
            if (container is null)
                image.Sprite = null;
            else
            {
                image.SetElement(
                    container.First,
                    container.Scale * Size,
                    container.AnchorFirst * MultipleSize);
                container.SetFirstImage(image);
            }
        }

        protected void SetElement(AvatarElementSyImage image, AvatarSpriteContainer container,
            Vector2 vector)
        {
            if (container is null)
                image.Sprite = null;
            else
            {
                image.SetElement(
                    container.First,
                    container.AnchorFirst * MultipleSize,
                    container.Scale * Size,
                    vector);
                container.SetFirstImage(image);
            }
        }

        protected void SetElement(
            AvatarElementXySrImage lft, AvatarElementXySrImage rht,
            RoleAvatar.AvatarElement4 element, AvatarSpriteContainer2 container)
        {
            if (container is null)
            {
                lft.Sprite = null;
                rht.Sprite = null;
            }
            else
            {
                Vector4 vector = new(
                    GetRange11(element.X),
                    GetRange11(element.Y),
                    GetRange01(element.Z),
                    GetRange01(element.W)
                );

                lft.SetElement(
                    container.First,
                    container.AnchorFirst * MultipleSize,
                    container.Scale * Size, vector);
                container.SetFirstImage(lft);

                vector.x = -vector.x;
                vector.w = 1 - vector.w;

                rht.SetElement(
                    container.Second,
                    container.AnchorSecond * MultipleSize,
                    container.Scale * Size, vector);
                container.SetSecondImage(rht);
            }
        }

        protected float GetRange01(byte value)
        {
            return value / 255f;
        }

        protected float GetRange11(byte value)
        {
            return (value / 127.5f - 1) * MultipleSize;
        }

        protected virtual void SetAvatar(Role.BasicProperty basic)
        {
            Group = basic.Female ? Resource.Female : Resource.Male;
            RoleAvatar avatar = basic.Avatar;
            SetBackHair(avatar.BackHair);
            SetHead(avatar.Head);
            SetHeadFeature(avatar.HeadFeature);
            SetNose(avatar.NoseMouth);
            SetMouth(avatar.NoseMouth);
            SetEye(avatar.Eye);
            SetBrow(avatar.Brow);
            SetFrontHair(avatar.FrontHair);
            if (RoleName)
                RoleName.text = basic.Name;
        }
        
        public virtual void Clear()
        {
            BackHair.Sprite = null;
            Head.Sprite = null;
            HeadFeature.Sprite = null;
            Nose.Sprite = null;
            Mouth.Sprite = null;
            LeftBrow.Sprite = null;
            RightBrow.Sprite = null;
            RightEye.Sprite = null;
            LeftEye.Sprite = null;
            FrontHair.Sprite = null;
            if (RoleName)
                RoleName.text = "";
        }
    }
}