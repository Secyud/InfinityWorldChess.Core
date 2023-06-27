using Secyud.Ugf.DependencyInjection;

namespace InfinityWorldChess.ArchivingDomain
{
	[Registry]
	public class ArchivingContext
	{
		public ArchivingContext()
		{
			ArchivingSlot[] slots =  new ArchivingSlot[SharedConsts.SlotsCount];
			for (int i = 0; i < SharedConsts.SlotsCount; i++)
			{
				slots[i] = new ArchivingSlot(i);
			}
			Slots = slots;
		}
		
		public ArchivingSlot[]  Slots { get; } 

		public ArchivingSlot CurrentSlot { get; set; }

		}
}