using InfinityWorldChess.BuffDomain;
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
    ///
    /// the four property mean different thing.
    /// living means state.
    /// kiling means difficulty.
    /// nimble means index in group.
    /// defend 
    /// </summary>
    public interface IActivity:IInstallable,IShowable,IHasContent,IDataResource,IPropertyAttached,IAttachProperty
    {
        
        
    }
}