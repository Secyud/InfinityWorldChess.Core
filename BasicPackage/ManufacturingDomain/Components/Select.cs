using System.Collections.Generic;
using InfinityWorldChess.GlobalDomain;
using InfinityWorldChess.Ugf;
using Secyud.Ugf;
using Secyud.Ugf.TableComponents.FilterComponents;
using Secyud.Ugf.TableComponents.SorterComponents;
using UnityEngine;

namespace InfinityWorldChess.ManufacturingDomain.Components
{
    public interface ISelect<out TItem>
    {
        public TItem SelectedItem { get; }
    }

    public abstract class Select<TItem, TSorter, TFilter> :
        ManufactureComponentBase, ISelect<TItem>
        where TItem : class, IShowable
        where TSorter : SorterRegeditBase<TItem>
        where TFilter : FilterRegeditBase<TItem>
    {
        [SerializeField] private ShownCell ShownCell;

        public ShownCell Cell => ShownCell;
        public TItem SelectedItem { get; private set; }

        public void OnSelectClick()
        {
            GlobalScope.Instance.OpenSelect()
                .AutoSetSingleSelectTable<TItem, TSorter, TFilter>(
                    GetSelectList(), ChangeSelect);
        }

        protected virtual void ChangeSelect(TItem select)
        {
            SelectedItem = select;
            ShownCell.BindShowable(select);
        }

        protected abstract IList<TItem> GetSelectList();
    }
}