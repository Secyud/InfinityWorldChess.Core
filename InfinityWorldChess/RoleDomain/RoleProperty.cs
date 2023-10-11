namespace InfinityWorldChess.RoleDomain
{
	public abstract class RoleProperty
	{
		public Role Role { get; internal set; }

		public abstract bool CheckNeeded();
	}
}