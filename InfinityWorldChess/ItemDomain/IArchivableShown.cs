#region

using Secyud.Ugf;
using UnityEngine;

#endregion

namespace InfinityWorldChess.ItemDomain
{
	public interface IArchivableShown
	{
		string Name { get; set; }

		string Description { get; set; }

		IObjectAccessor<Sprite> Icon { get; set; }
	}
}