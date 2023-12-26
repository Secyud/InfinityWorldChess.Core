namespace InfinityWorldChess.BuffDomain
{
    public static class BuffExtension
    {
        public static void TryAttach(this IAttachProperty property, object obj)
        {
            if (obj is IPropertyAttached propertyAttached)
            {
                propertyAttached.Property = property;
            }
        }

        public static byte Get(this IAttachProperty property, int index)
        {
            return index switch
            {
                0 => property.Living,
                1 => property.Kiling,
                2 => property.Nimble,
                3 => property.Defend,
                _ => 0
            };
        }
    }
}