namespace InfinityWorldChess.BuffDomain
{
    public interface IAttachProperty
    {
        // 生杀灵御
        public int Living { get; set; }

        public int Kiling { get; set; }

        public int Nimble { get; set; }

        public int Defend { get; set; }
    }
}