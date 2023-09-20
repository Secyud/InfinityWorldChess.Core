using InfinityWorldChess.ActivityDomain;
using InfinityWorldChess.PlayerDomain;
using Secyud.Ugf.TabComponents;
using UnityEngine;

namespace InfinityWorldChess.GameDomain.GameMenuDomain
{
    public class GameMenuActivityTab : TabPanel
    {
        [SerializeField] private ActivityPanel Panel;

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

            Panel.Bind(player.Activity);
        }
    }
}