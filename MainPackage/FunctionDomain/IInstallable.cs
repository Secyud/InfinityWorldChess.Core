namespace InfinityWorldChess.FunctionDomain
{
    public interface IInstallable<in TTarget>
    {
        void InstallFrom(TTarget target);

        void UnInstallFrom(TTarget target);
    }
    public interface IInstallable
    {
        void Install();

        void UnInstall();
    }

    public interface IActionInstallTarget 
    {
        void Install(IActionable actionable);

        void UnInstall(IActionable actionable);
    }
    public interface IActionInstallTarget<out TTarget>
    {
        void Install(IInstallable<TTarget> installable);

        void UnInstall(IInstallable<TTarget> installable);
    }
}