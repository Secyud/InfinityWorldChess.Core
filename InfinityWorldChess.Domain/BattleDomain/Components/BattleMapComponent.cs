#region

using Secyud.Ugf.HexMap;
using UnityEngine;
using UnityEngine.Animations;

#endregion

namespace InfinityWorldChess.BattleDomain
{
	public class BattleMapComponent : HexMapRootBase
	{
		[SerializeField] private LookAtConstraint BillBoardPrefab;

		private void Update()
		{
			BattleScope.Context.OnUpdate();
		}

		public void AddBillBoard(HexCell cell,Transform t,float height = 10)
		{
			LookAtConstraint c = BillBoardPrefab.Instantiate(cell.transform);
			c.transform.localPosition = new Vector3(0,height,0);
			c.SetSource(0,new ConstraintSource
			{
				sourceTransform = Camera.transform,
				weight = 1
			});
			t.SetParent(c.transform);
			t.localPosition = Vector3.zero;
			t.rotation = Quaternion.Euler(0,180,0);
		}
	}
}