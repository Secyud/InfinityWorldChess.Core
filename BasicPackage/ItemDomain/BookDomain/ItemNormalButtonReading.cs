#region

using Secyud.Ugf.TableComponents.ButtonComponents;

#endregion

namespace InfinityWorldChess.ItemDomain.BookDomain
{
	public class ItemNormalButtonReading : ButtonDescriptor<IItem>
	{
		public override string ShowName => "研读";

		public override void Trigger()
		{
			(Target as IReadable)!.Reading();
		}

		public override bool Visible(IItem target)
		{
			return target is IReadable;
		}
	}
}