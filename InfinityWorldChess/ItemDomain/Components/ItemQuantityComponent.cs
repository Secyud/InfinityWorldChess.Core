using Secyud.Ugf.BasicComponents;
using Secyud.Ugf.TableComponents;
using UnityEngine;

namespace InfinityWorldChess.ItemDomain
{
    public class ItemQuantityComponent : MonoBehaviour
    {
        [SerializeField] private SText QuantityText;
        [SerializeField] private GameObject Quantity;

        public void SetQuantity(IItem item)
        {
            if (item is IOverloadedItem overloaded)
            {
                QuantityText.text = overloaded.Quantity.ToString();
            }
            else
            {
                Quantity.SetActive(false);
            }
        }

        public static void InitQuantity(TableCell cell,IItem item)
        {
            ItemQuantityComponent component = cell.GetComponent<ItemQuantityComponent>();
            component.SetQuantity(item);
        } 

        public static void SetItem(TableDelegate<IItem> table)
        {
            table.BindInitAction(InitQuantity);
        }
    }
}