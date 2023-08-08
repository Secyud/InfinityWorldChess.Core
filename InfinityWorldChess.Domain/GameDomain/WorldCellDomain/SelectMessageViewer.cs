#region

using Secyud.Ugf.BasicComponents;
using UnityEngine;

#endregion

namespace InfinityWorldChess.GameDomain.WorldCellDomain
{
    public class SelectMessageViewer : MonoBehaviour
    {
        [SerializeField] private SText Position;
        [SerializeField] private SText[] Resources;

        private SelectRefreshItem _item;

        private void Awake()
        {
            _item ??= new SelectRefreshItem(nameof(SelectMessageViewer), Refresh);
        }

        private void Refresh()
        {
            WorldCell cell = GameScope.Instance.Get<SelectRefreshService>().Cell;
            if (cell is not null)
            {
                Position.Set(cell.Cell.Coordinates.ToString());
                Resources.Set(
                    cell.Stone.ToString(),
                    cell.Tree.ToString(),
                    cell.Farm.ToString()
                );
            }
            else
            {
                Position.Set(string.Empty);
                Resources.Set(string.Empty, string.Empty, string.Empty);
            }
        }
    }
}