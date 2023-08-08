using Secyud.Ugf;
using UnityEngine;

namespace InfinityWorldChess.Ugf
{
	public class FunctionService:IFunctionService
	{
		public RectTransform CreateFloatingForContent(IHasContent content)
		{
			return content.CreateAutoCloseFloatingOnMouse();
		}
	}
}