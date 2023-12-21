namespace InfinityWorldChess
{
    public static class BPC
    {
        public static string P(float f, string s1 = null, string s2 = null)
        {
            return f > 0 ? $"增加{f:P0}{s1}" : $"减少{-f:P0}{s2}";
        }

        public static string F(float i)
        {
            return i > 0 ? $"增加{i}" : $"减少{-i}";
        }
    }
}