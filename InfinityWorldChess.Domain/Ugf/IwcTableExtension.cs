using System;
using System.Collections.Generic;
using Secyud.Ugf;
using Secyud.Ugf.TableComponents;
using Secyud.Ugf.TableComponents.ButtonComponents;
using Secyud.Ugf.TableComponents.FilterComponents;
using Secyud.Ugf.TableComponents.PagerComponents;
using Secyud.Ugf.TableComponents.SelectComponents;
using Secyud.Ugf.TableComponents.SorterComponents;

namespace InfinityWorldChess.Ugf
{
    public static class IwcTableExtension
    {
        public static void SetEnabledSelect<TItem>(TableCell cell, TItem item, bool select)
        {
            ((ShownCell)cell).Select.enabled = select;
        }

        public static void SetShownCell<TItem>(TableCell cell, TItem item)
        {
            ((ShownCell)cell).BindShowable((IShowable)item);
        }

        public static TableDelegate<TItem> AutoSetTable<TItem>(
            this Table table, IList<TItem> items, TableCell cellTemplate,
            Action<TableCell, TItem> cellSetAction = null)
        {
            TableDelegate<TItem> tableDelegate = TableDelegate<TItem>.Create(table, items, cellTemplate);
            tableDelegate.BindInitAction(cellSetAction ?? SetShownCell);
            if (table[nameof(Pager)])
                PagerDelegate<TItem>.Create(table);
            return tableDelegate;
        }


        public static TableDelegate<TItem> AutoSetFunctionTable<TItem, TSorterService, TFilterService>(
            this Table table, IList<TItem> items, TableCell cellTemplate,
            Action<TableCell, TItem> cellSetAction = null)
            where TSorterService : SorterRegeditBase<TItem>
            where TFilterService : FilterRegeditBase<TItem>
        {
            TableDelegate<TItem> tableDelegate = AutoSetTable(table, items, cellTemplate, cellSetAction);
            SorterDelegate<TItem>.Create<TSorterService>(table);
            FilterDelegate<TItem>.Create<TFilterService>(table);
            return tableDelegate;
        }

        public static TableButtonDelegate<TItem> AutoSetButtonTable<TItem, TSorterService, TFilterService,
            TButtonService>(
            this Table table, IList<TItem> items, TableCell cellTemplate,
            Action<TableCell, TItem> cellSetAction = null)
            where TSorterService : SorterRegeditBase<TItem>
            where TFilterService : FilterRegeditBase<TItem>
            where TButtonService : ButtonRegeditBase<TItem>
        {
            AutoSetFunctionTable<TItem, TSorterService, TFilterService>(table, items, cellTemplate, cellSetAction);
            return TableButtonDelegate<TItem>.Create<TButtonService>(table);
        }

        public static MultiSelectDelegate<TItem> AutoSetMultiSelectTable<TItem, TSorterService, TFilterService>(
            this Table table, IList<TItem> items, TableCell cellTemplate, IList<TItem> bindSelectItems,
            Action<TableCell, TItem, bool> setSelectAction = null,
            Action<TableCell, TItem> cellSetAction = null)
            where TSorterService : SorterRegeditBase<TItem>
            where TFilterService : FilterRegeditBase<TItem>
        {
            AutoSetFunctionTable<TItem, TSorterService, TFilterService>(table, items, cellTemplate, cellSetAction);
            MultiSelectDelegate<TItem> ret = MultiSelectDelegate<TItem>.Create(table, bindSelectItems);
            ret.BindSelectSetAction(setSelectAction ?? SetEnabledSelect);
            return ret;
        }

        public static SingleSelectDelegate<TItem> AutoSetSingleSelectTable<TItem, TSorterService, TFilterService>(
            this Table table, IList<TItem> items, TableCell cellTemplate, Action<TItem> ensureAction,
            Action<TableCell, TItem> cellSetAction = null)
            where TSorterService : SorterRegeditBase<TItem>
            where TFilterService : FilterRegeditBase<TItem>
            where TItem : class
        {
            AutoSetFunctionTable<TItem, TSorterService, TFilterService>(table, items, cellTemplate, cellSetAction);
            SingleSelectDelegate<TItem> ret = SingleSelectDelegate<TItem>.Create(table);
            ret.BindEnsureAction(ensureAction);
            return ret;
        }

        public static SorterDelegate<TItem> AutoSetSorterTable<TItem, TSorterService>(
            this Table table, IList<TItem> items, TableCell cellTemplate,
            Action<TableCell, TItem> cellSetAction = null)
            where TSorterService : SorterRegeditBase<TItem>
        {
            AutoSetTable(table, items, cellTemplate, cellSetAction);
            return SorterDelegate<TItem>.Create<TSorterService>(table);
        }

        public static FilterDelegate<TItem> AutoSetFilterTable<TItem, TFilterService>(
            this Table table, IList<TItem> items, TableCell cellTemplate,
            Action<TableCell, TItem> cellSetAction = null)
            where TFilterService : FilterRegeditBase<TItem>
        {
            AutoSetTable(table, items, cellTemplate, cellSetAction);
            return FilterDelegate<TItem>.Create<TFilterService>(table);
        }
    }
}