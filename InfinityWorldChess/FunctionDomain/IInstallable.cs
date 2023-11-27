namespace InfinityWorldChess.FunctionDomain
{
    public interface IInstallable<in TTarget>
    {
        void InstallFrom(TTarget target);

        void UnInstallFrom(TTarget target);
    }
    public interface IInstallable
    {
        void InstallFrom();

        void UnInstallFrom();
    }

    public interface IActionInstallTarget 
    {
        void Install(IActionable installable);

        void UnInstall(IActionable installable);
    }
    public interface IActionInstallTarget<out TTarget>
    {
        void Install(IInstallable<TTarget> installable);

        void UnInstall(IInstallable<TTarget> installable);
    }
}