using Secyud.Ugf;
using Secyud.Ugf.DependencyInjection;
using UnityEngine;

namespace InfinityWorldChess.Ugf
{
	[Registry]
	public class FunctionService:IFunctionService
	{
		public RectTransform CreateFloatingForContent(IHasContent content)
		{
			return content.CreateAutoCloseFloatingOnMouse();
		}
	}
}