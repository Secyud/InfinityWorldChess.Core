using InfinityWorldChess.FunctionDomain;

namespace InfinityWorldChess.BuffDomain
{
    public static class BuffExtension
    {
        public static void Attach(this IAttachProperty property, object obj)
        {
            if (obj is IPropertyAttached propertyAttached)
            {
                propertyAttached.Property = property;
            }
        }
    }
}