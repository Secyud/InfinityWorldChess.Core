using Secyud.Ugf;
using Secyud.Ugf.Archiving;
using System.IO;

namespace InfinityWorldChess.ArchivingComponent
{
	public class IwcArchivingContext:IArchivingContext
	{
		public IwcArchivingContext()
		{
			ISlot[] slots =  new ISlot[SharedConsts.SlotsCount];
			for (int i = 0; i < SharedConsts.SlotsCount; i++)
			{
				slots[i] = new IwcSlot(i);
			}
			Slots = slots;
		}
		
		public ISlot[]  Slots { get; } 

		public ISlot CurrentSlot { get; set; }

		public bool CurrentSlotExist => 
			Directory.Exists(Path.Combine(Og.ArchivingPath, CurrentSlot.Id.ToString()));
	}
}