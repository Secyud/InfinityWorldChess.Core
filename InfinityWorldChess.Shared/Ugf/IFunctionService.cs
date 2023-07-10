using Secyud.Ugf;
using Secyud.Ugf.DependencyInjection;
using UnityEngine;

namespace InfinityWorldChess.Ugf
{
	public interface IFunctionService
	{
		RectTransform CreateFloatingForContent(IHasContent content);
	}
}