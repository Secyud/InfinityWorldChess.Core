#region

using InfinityWorldChess.GlobalDomain;
using Secyud.Ugf;
using Secyud.Ugf.Archiving;

#endregion

namespace InfinityWorldChess.ActivityDomain
{
	public interface IActivity : ICanBeShown, IHasContent,IArchivable,IHasSaveIndex
	{
		void OnReceive();
	
		IActivityGroup Group { get; }
		
		ActivityState State { get; }
	}
}