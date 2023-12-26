namespace InfinityWorldChess.RoleDomain
{
    public class AvatarElementMessage
    {
        public AvatarElementType Type { get; }
        public float RotateMax { get; }
        public float RotateMin { get; }
        public float BiasX { get; }
        public float XRange { get; }
        public float BiasY { get; }
        public float YRange { get; }
        public float ScaleMax { get; }
        public float ScaleMin { get; }

        public AvatarElementMessage(AvatarElementType type,
            float rotateMax = 180, float rotateMin = -180,
            float biasX = 0, float xRange = 0.1f,
            float biasY = 0, float yRange = 0.1f,
            float scaleMax = 1.5f, float scaleMin = 0.5f)
        {
            Type = type;
            RotateMax = rotateMax;
            RotateMin = rotateMin;
            BiasX = biasX;
            XRange = xRange;
            BiasY = biasY;
            YRange = yRange;
            ScaleMax = scaleMax;
            ScaleMin = scaleMin;
        }


        public AvatarElementMessage MergeMessage(AvatarElementMessage other)
        {
            if (other is null)
                return this;
            return new AvatarElementMessage(Type,
                RotateMax * other.RotateMax, RotateMin * other.RotateMin,
                BiasX - other.BiasX, XRange * other.XRange,
                BiasY - other.BiasY, YRange * other.YRange,
                ScaleMax * other.ScaleMax, ScaleMin * other.ScaleMin);
        }

        public static readonly AvatarElementMessage[] Messages =
        {
            new(AvatarElementType.BckFtr, biasX: 0, biasY: 0.176f),
            new(AvatarElementType.BckHar, biasX: 0, biasY: -.266f),
            new(AvatarElementType.FntBdy, biasX: 0, biasY: 0.176f),
            new(AvatarElementType.FntHad, biasX: 0, biasY: -.266f),
            new(AvatarElementType.HadFtr, biasX: 0, biasY: -.266f),
            new(AvatarElementType.NseFtr, biasX: 0, biasY: -.236f,yRange:0.01f),
            new(AvatarElementType.MthFtr, biasX: 0, biasY: -.187f,yRange:0.01f),
            new(AvatarElementType.LftEye, -30, 30, 0.047f, 0.01f, -.256f, 0.01f),
            new(AvatarElementType.RhtEye, 30, -30, -.047f, -.01f, -.256f, 0.01f),
            new(AvatarElementType.LftBrw, -30, 30, 0.047f, 0.01f, -.275f, 0.01f),
            new(AvatarElementType.RhtBrw, 30, -30, -.047f, -.01f, -.275f, 0.01f),
            new(AvatarElementType.FntHar, biasX: 0, biasY: -.266f),
        };
    }
}