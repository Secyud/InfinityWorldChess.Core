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

        private void Awake()
        {
            GameScope.Instance.Get<SelectObservedService>()
                .AddObserverObject(nameof(SelectMessageViewer), Refresh,gameObject);
        }

        private void Refresh()
        {
            WorldCell cell = GameScope.Instance.Get<SelectObservedService>().Cell;
            if (cell)
            {
                Position.Set(": " + cell.Coordinates);
            }
            else
            {
                Position.Set(string.Empty);
                Resources.Set(":", ":", ":", ":");
            }
        }
    }
}