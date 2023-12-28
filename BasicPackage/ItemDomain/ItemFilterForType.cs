#region

using Secyud.Ugf.TableComponents.FilterComponents;

#endregion

namespace InfinityWorldChess.ItemDomain
{
	public class ItemFilterForType<TItemType> : FilterToggleDescriptor<IItem>
	{
		public override bool Filter(IItem target)
		{
			return target is TItemType;
		}
	}
}