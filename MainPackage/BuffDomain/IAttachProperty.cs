namespace InfinityWorldChess.BuffDomain
{
    public interface IAttachProperty
    {
        // 生杀灵御
        public byte Living { get; set; }

        public byte Kiling { get; set; }

        public byte Nimble { get; set; }

        public byte Defend { get; set; }
    }
}