using InfinityWorldChess.Ugf;
using Secyud.Ugf;
using Secyud.Ugf.BasicComponents;
using UnityEngine;

namespace InfinityWorldChess.ManufacturingDomain
{
	public class FlavorCell:ImageCell
	{
		[SerializeField] public SCircleImage Flavor;

		public void OnInitialize(IFlavorProcess process)
		{
			base.OnInitialize(process);
			SetFloating(process,Icon.gameObject);
			if (process.Flavor is ICanBeShown shown)
			{
				Flavor.Sprite = shown.ShowIcon?.Value;
				if (process.Flavor is IHasContent content)
					SetFloating(content,Flavor.gameObject);
			}
			else
			{
				Flavor.Sprite = null;
			}
		}
	}
}