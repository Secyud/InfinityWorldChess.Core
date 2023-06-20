#region

using Secyud.Ugf.TypeHandle;
using System.Collections.Generic;

#endregion

namespace InfinityWorldChess.BuffDomain
{
	public interface IParasitifer<TTarget>
		where TTarget : class, IParasitifer<TTarget>
	{
		SortedDictionary<TypeDescriptor, IParasiteBuff<TTarget>> Parasites { get; }
	}
}