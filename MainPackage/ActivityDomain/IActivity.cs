using InfinityWorldChess.FunctionDomain;
using Secyud.Ugf;
using Secyud.Ugf.Archiving;

namespace InfinityWorldChess.ActivityDomain
{
    /// <summary>
    /// activity is an equippable of role.
    /// install means role access the activity.
    /// triggers will be added;
    ///
    /// uninstall means role finished the activity.
    /// activity is attached to activity group.
    /// </summary>
    public interface IActivity:IInstallable,IShowable,IHasContent,IDataResource,IArchivable
    {
        ActivityState State { get; set; }
        
    }
}