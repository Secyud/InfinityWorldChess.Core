using Secyud.Ugf;
using Secyud.Ugf.DependencyInjection;
using UnityEngine;

namespace InfinityWorldChess.Ugf
{
	public class IwcFunctionService:IFunctionService,ISingleton
	{
		public RectTransform CreateFloatingForContent(IHasContent content)
		{
			return content.CreateAutoCloseFloatingOnMouse();
		}
	}
}