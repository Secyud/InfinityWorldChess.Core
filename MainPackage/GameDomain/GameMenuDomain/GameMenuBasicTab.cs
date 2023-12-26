using InfinityWorldChess.PlayerDomain;
using InfinityWorldChess.RoleDomain;
using Secyud.Ugf.TabComponents;
using UnityEngine;

namespace InfinityWorldChess.GameDomain.GameMenuDomain
{
    public class GameMenuBasicTab : TabPanel
    {
        [SerializeField] private NameEditor NameEditor;
        [SerializeField] private GenderEditor GenderEditor;
        [SerializeField] private BodyPartEditor BodyPartEditor;
        [SerializeField] private BirthEditor BirthEditor;
        [SerializeField] private AvatarEditor AvatarEditor;

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

            NameEditor.Bind(player.Role.Basic);
            GenderEditor.Bind(player.Role.Basic);
            BodyPartEditor.Bind(player.Role.BodyPart);
            BirthEditor.Bind(player.Role.Basic);
            AvatarEditor.OnInitialize(player.Role.Basic);
        }
    }
}