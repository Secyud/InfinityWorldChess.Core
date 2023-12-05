using Secyud.Ugf;
using UnityEngine;

namespace InfinityWorldChess.Ugf
{
    public static class IwcObjectExtension
    {
        public static void TrySetContent(this object @object, Transform transform)
        {
            if (@object is IHasContent content)
            {
                content.SetContent(transform);
            }
        }
    }
}