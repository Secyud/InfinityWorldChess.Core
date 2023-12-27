using System.Collections.Generic;
using InfinityWorldChess.GameDomain;
using InfinityWorldChess.ItemDomain;
using InfinityWorldChess.PlayerDomain;
using Secyud.Ugf.TableComponents;
using UnityEngine;
using UnityEngine.Events;

namespace InfinityWorldChess.BattleDomain
{
    public class BattleFinishedPanel:MonoBehaviour
    {
        [SerializeField] private Table ItemTable;
        [SerializeField] private UnityEvent<string> Award;
        [SerializeField] private UnityEvent<string> Experience;

        private List<IItem> _items;
        private int _award;
        private int _experience;
        private void Awake()
        {
            _items = new List<IItem>();
            ItemTable.AutoSetTable(_items);
        }

        public void AddBattleFinished(List<IItem> items,int award,int experience)
        {
            _items.AddRange(items);
            ItemTable.Refresh();
            _award += award;
            _experience += experience;
            Award.Invoke(_award.ToString());
            Award.Invoke(_experience.ToString());

            PlayerGameContext player = GameScope.Instance.Player;
            player.Role.Item.Award += award;
            player.Role.Basic.Level += experience;
            foreach (IItem item in items)
            {
                player.Role.Item.Add(item); 
            }
        }
    }
}