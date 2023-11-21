namespace InfinityWorldChess.BuffDomain
{
    public interface IEquippable<in TTarget>
    {
        void Install(TTarget target);

        void UnInstall(TTarget target);
    }
}