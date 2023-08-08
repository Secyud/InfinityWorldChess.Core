namespace InfinityWorldChess.RoleDomain
{
    public class AvatarElementMessage
    {
        public readonly AvatarElementType Type;
        public readonly bool Rotatable;
        public readonly float RotateMax;
        public readonly float RotateMin;
        public readonly bool MovableX;
        public readonly float BiasX;
        public readonly float XRange;
        public readonly bool MovableY;
        public readonly float BiasY;
        public readonly float YRange;
        public readonly bool Scalable;
        public readonly float ScaleMax;
        public readonly float ScaleMin;

        public AvatarElementMessage(AvatarElementType type,
            bool rotatable = false, float rotateMax = 360, float rotateMin = 0,
            bool movableX = false, float biasX = 0, float xRange = -0.1f,
            bool movableY = false,float biasY = 0, float yRange = 0.1f,
            bool scalable = false, float scaleMax = 1.5f, float scaleMin = 0.5f)
        {
            Type = type;
            Rotatable = rotatable;
            RotateMax = rotateMax;
            RotateMin = rotateMin;
            MovableX = movableX;
            BiasX = biasX;
            XRange = xRange;
            MovableY = movableY;
            BiasY = biasY;
            YRange = yRange;
            Scalable = scalable;
            ScaleMax = scaleMax;
            ScaleMin = scaleMin;
        }


        public AvatarElementMessage MergeMessage(AvatarElementMessage other)
        {
            if (other is null)
                return this;
            return new AvatarElementMessage(Type,
                Rotatable, RotateMax * other.RotateMax, RotateMin * other.RotateMin,
                MovableX, BiasX + other.BiasX, XRange * other.XRange, 
                MovableY,BiasY + other.BiasY,YRange * other.YRange,
                Scalable, ScaleMax * other.ScaleMax, ScaleMin * other.ScaleMin);
        }

        public static AvatarElementMessage[] Messages =
        {
            new(AvatarElementType.BckFtr, rotatable: true, movableX: true, scalable: true),
            new(AvatarElementType.BckHar),
            new(AvatarElementType.FntBdy),
            new(AvatarElementType.FntHad),
            new(AvatarElementType.HadFtr, rotatable: true, movableX: true, scalable: true),
            new(AvatarElementType.NseFtr, movableX: true, scalable: true),
            new(AvatarElementType.MthFtr, movableX: true, scalable: true),
            new(AvatarElementType.LftEye, rotatable: true, movableX: true, scalable: true),
            new(AvatarElementType.RhtEye, rotatable: true, movableX: true, scalable: true),
            new(AvatarElementType.LftBrw, rotatable: true, movableX: true, scalable: true),
            new(AvatarElementType.RhtBrw, rotatable: true, movableX: true, scalable: true),
            new(AvatarElementType.FntHar),
        };
    }
}