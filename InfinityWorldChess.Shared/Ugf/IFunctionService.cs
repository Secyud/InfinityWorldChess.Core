using Secyud.Ugf;
using Secyud.Ugf.DependencyInjection;
using UnityEngine;

namespace InfinityWorldChess.Ugf
{
	[Registry]
	public interface IFunctionService
	{
		RectTransform CreateFloatingForContent(IHasContent content);
	}
}