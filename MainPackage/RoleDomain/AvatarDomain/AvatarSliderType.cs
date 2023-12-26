using System;

namespace InfinityWorldChess.RoleDomain
{
    [Serializable]
    public enum AvatarSliderType:byte
    {
        Sprite,
        PositX,
        PositY,
        PositXY,
        Rotate,
        Scale,
    }
}