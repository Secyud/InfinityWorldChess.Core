namespace InfinityWorldChess.ManufacturingDomain
{
	public interface IFlavorManufacturingData
	{
		public void OnAddProcessClick();
		public void SetTime(float f);
	
		public void OnShowResultButtonClick();

		public void OnManufactureButtonClick();

		public void OnShutdown();

		public void SetName(string text);
	}
}