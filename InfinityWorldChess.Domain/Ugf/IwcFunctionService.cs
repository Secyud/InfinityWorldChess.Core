using Secyud.Ugf;
using UnityEngine;

namespace InfinityWorldChess.Ugf
{
	public class IwcFunctionService:IFunctionService
	{
		public RectTransform CreateFloatingForContent(IHasContent content)
		{
			return content.CreateAutoCloseFloatingOnMouse();
		}
	}
}