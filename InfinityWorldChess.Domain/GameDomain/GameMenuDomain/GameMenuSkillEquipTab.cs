using InfinityWorldChess.PlayerDomain;
using InfinityWorldChess.SkillDomain;
using UnityEngine;

namespace InfinityWorldChess.GameDomain.GameMenuDomain
{
    public class GameMenuSkillEquipTab : MonoBehaviour
    {
        [SerializeField] private CoreSkillView CoreSkillView;
        [SerializeField] private FormSkillView FormSkillView;
        [SerializeField] private PassiveSkillView PassiveSkillView;

        private GameMenuTabItem _tabItem;
        private void Awake()
        {
            _tabItem ??= new GameMenuTabItem(nameof(GameMenuSkillEquipTab), gameObject, Refresh);
        }
        
        public  void Refresh()
        {
            PlayerGameContext player = GameScope.Instance.Player;

            CoreSkillView.Bind(player.Role);
            FormSkillView.Bind(player.Role);
            PassiveSkillView.Bind(player.Role);
        }
    }
}