#region

using UnityEngine;

#endregion

namespace InfinityWorldChess.RoleDomain
{
	public class NatureEditor : MonoBehaviour
	{
		private Role.NatureProperty _natureProperty;

		public void SetRecognize(float value)
		{
			_natureProperty.Recognize = value;
		}

		public void SetStability(float value)
		{
			_natureProperty.Recognize = value;
		}

		public void SetConfident(float value)
		{
			_natureProperty.Confident = value;
		}

		public void SetEfficient(float value)
		{
			_natureProperty.Efficient = value;
		}

		public void SetGregarious(float value)
		{
			_natureProperty.Gregarious = value;
		}

		public void SetAltruistic(float value)
		{
			_natureProperty.Altruistic = value;
		}

		public void SetRationality(float value)
		{
			_natureProperty.Rationality = value;
		}

		public void SetIntelligent(float value)
		{
			_natureProperty.Intelligent = value;
		}

		public void SetForesighted(float value)
		{
			_natureProperty.Foresighted = value;
		}

		public void OnInitialize(Role.NatureProperty natureProperty)
		{
			_natureProperty = natureProperty;
		}
	}
}