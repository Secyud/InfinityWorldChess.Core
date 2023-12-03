using System.Collections.Generic;
using InfinityWorldChess.GameDomain;
using InfinityWorldChess.ItemDomain;
using InfinityWorldChess.PlayerDomain;
using InfinityWorldChess.Ugf;
using Secyud.Ugf.TableComponents;
using UnityEngine;
using UnityEngine.Events;

namespace InfinityWorldChess.BattleDomain
{
    public class BattleFinishedPanel:MonoBehaviour
    {
        [SerializeField] private Table ItemTable;
        [SerializeField] private UnityEvent<string> Award;

        private List<IItem> _items;
        private int _award;
        private void Awake()
        {
            _items = new List<IItem>();
            ItemTable.AutoSetTable(_items);
        }

        public void AddBattleFinished(List<IItem> items,int award,int skillPoints)
        {
            _items.AddRange(items);
            ItemTable.Refresh();
            _award += award;
            Award.Invoke(_award.ToString());

            PlayerGameContext player = GameScope.Instance.Player;
            player.Role.Item.Award += award;
            foreach (IItem item in items)
            {
                player.Role.Item.Add(item); 
            }
        }
    }
}