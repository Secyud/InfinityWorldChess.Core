#region

using Secyud.Ugf.DependencyInjection;
using UnityEngine;

#endregion

namespace InfinityWorldChess.InputDomain
{
	public interface IInputKeyMapService : ISingleton
	{
		KeyCode this[InputKeyWord word] { get; set; }
	}
}