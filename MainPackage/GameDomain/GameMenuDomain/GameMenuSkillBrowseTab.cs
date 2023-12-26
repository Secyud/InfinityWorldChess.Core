using InfinityWorldChess.PlayerDomain;
using InfinityWorldChess.SkillDomain;
using Secyud.Ugf.TabComponents;
using UnityEngine;

namespace InfinityWorldChess.GameDomain.GameMenuDomain
{
    public class GameMenuSkillBrowseTab : TabPanel
    {
        [SerializeField] private SkillBrowseTabs SkillBrowseTabs;
        private GameMenuTabService _service;
        protected override TabService Service => _service;

        protected override void Awake()
        {
            _service = GameScope.Instance.Get<GameMenuTabService>();
            base.Awake();
        }

        public override void RefreshTab()
        {
            PlayerGameContext player = GameScope.Instance.Player;
            SkillBrowseTabs.OnInitialize(player.Role);
        }
    }
}