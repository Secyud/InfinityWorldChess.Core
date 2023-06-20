using Secyud.Ugf;
using UnityEngine;

namespace InfinityWorldChess.Ugf
{
    public static class FunctionServiceExtension
    {
        private static IFunctionService _service;
        private static IFunctionService Service => _service ??= Og.DefaultProvider.Get<IFunctionService>();

        public static RectTransform CreateFloating(this IHasContent content)
        {
            return Service.CreateFloatingForContent(content);
        }
    }
}