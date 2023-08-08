using Secyud.Ugf.BasicComponents;
using UnityEngine;

namespace InfinityWorldChess.GameDomain.WorldCellDomain
{
    public class CurrentFunctionsTab : MonoBehaviour
    {
        [SerializeField] private SButtonGroup ButtonGroup;

        private CurrentTabItem _item;

        private void Awake()
        {
            _item ??= new CurrentTabItem(nameof(CurrentFunctionsTab), gameObject, Refresh);
        }

        public void Refresh()
        {
            ButtonGroup.Clear();

            WorldCell cell = GameScope.Instance.Get<CurrentTabService>().Cell;
            if (cell is not null)
            {
                ButtonGroup.OnInitialize(cell, cell.Buttons);
            }
        }
    }
}