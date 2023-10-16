using InfinityWorldChess.PlayerDomain;
using InfinityWorldChess.SkillDomain;
using Secyud.Ugf.TabComponents;
using UnityEngine;

namespace InfinityWorldChess.GameDomain.GameMenuDomain
{
    public class GameMenuSkillEquipTab : TabPanel
    {
        [SerializeField] private CoreSkillView CoreSkillView;
        [SerializeField] private FormSkillView FormSkillView;
        [SerializeField] private PassiveSkillView PassiveSkillView;
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

            CoreSkillView.Bind(player.Role);
            FormSkillView.Bind(player.Role);
            PassiveSkillView.Bind(player.Role);
        }
    }
}