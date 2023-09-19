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
        [SerializeField] private UnityEvent<string> SkillPoints;
        
        //TODO: A panel to show finish archive
        public void SetBattleFinished(List<IItem> items,int award,int skillPoints)
        {
            ItemTable.AutoSetTable(items);
            Award.Invoke(award.ToString());
            SkillPoints.Invoke(skillPoints.ToString());

            PlayerGameContext player = GameScope.Instance.Player;
            player.Role.Item.Award += award;
            player.SkillPoints += skillPoints;
            foreach (IItem item in items)
            {
                player.Role.Item.Add(item); 
            }
        }
    }
}