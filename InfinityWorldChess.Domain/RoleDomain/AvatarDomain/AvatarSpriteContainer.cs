#region

using Secyud.Ugf;
using Secyud.Ugf.AssetLoading;
using Secyud.Ugf.BasicComponents;
using UnityEngine;

#endregion

namespace InfinityWorldChess.RoleDomain
{
    public class AvatarSpriteContainer : IHasId<int>
    {
        private readonly SpriteContainer _first;
        public readonly Vector2 AnchorFirst;
        public readonly float Scale;
        public int Id { get; }
        public Sprite First => _first?.Value;

        public virtual void SetFirstImage(SImage image)
        {
        }


        public AvatarSpriteContainer(
            SpriteContainer sprite, int id,
            Vector2 anchorFirst = default, float scale = 1)
        {
            Scale = scale;
            AnchorFirst = anchorFirst;
            Id = id;
            _first = sprite;
        }
    }

    public class AvatarSpriteContainer2 : AvatarSpriteContainer
    {
        private readonly SpriteContainer _second;
        public readonly Vector2 AnchorSecond;
        public Sprite Second => _second?.Value;

        public AvatarSpriteContainer2(
            SpriteContainer lft, SpriteContainer rht, int id,
            Vector2 anchorFirst = default, Vector2 anchorSecond = default, float scale = 1)
            : base(lft, id, anchorFirst, scale)
        {
            _second = rht;
            AnchorSecond = anchorSecond;
        }

        public virtual void SetSecondImage(SImage image)
        {
        }
    }
}