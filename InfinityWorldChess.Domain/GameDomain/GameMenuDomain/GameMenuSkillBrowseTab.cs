using InfinityWorldChess.PlayerDomain;
using InfinityWorldChess.SkillDomain.SkillBrowseDomain;
using UnityEngine;

namespace InfinityWorldChess.GameDomain.GameMenuDomain
{
    public class GameMenuSkillBrowseTab : MonoBehaviour
    {
        [SerializeField] private SkillBrowseTabs SkillBrowseTabs;

        private GameMenuTabItem _tabItem;
        private void Awake()
        {
            _tabItem ??= new GameMenuTabItem(nameof(GameMenuSkillBrowseTab), gameObject, Refresh);
        }

        private void Refresh()
        {
            PlayerGameContext player = GameScope.Instance.Player;

            SkillBrowseTabs.gameObject.SetActive(true);
        }
        
        
    }
}