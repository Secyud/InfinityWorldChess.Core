#region

using Secyud.Ugf.TypeHandle;
using System.Collections.Generic;

#endregion

namespace InfinityWorldChess.BuffDomain
{
	public interface IParasitifer<TTarget>
		where TTarget : class, IParasitifer<TTarget>
	{
		SortedDictionary<TypeStruct, IParasiteBuff<TTarget>> Parasites { get; }
	}
}