#region

using Secyud.Ugf;
using Secyud.Ugf.AssetComponents;
using UnityEngine;

#endregion

namespace InfinityWorldChess.RoleDomain
{
    public class AvatarSpriteContainer : IHasId<int>
    {
        public int Id { get; }
        private readonly SpriteContainer _spriteContainer;
        private readonly AvatarElementType _type;
        public Sprite Sprite => _spriteContainer?.Value;
        public AvatarElementMessage Message { get; }

        public AvatarSpriteContainer(SpriteContainer sprite, int id,
            AvatarElementType type,
            AvatarElementMessage message = null)
        {
            Id = id;
            _type = type;
            Message = message is not null ? 
                AvatarElementMessage.Messages[(int)type].MergeMessage(message) :
                AvatarElementMessage.Messages[(int)type];

            _spriteContainer = sprite;
        }
    }
}