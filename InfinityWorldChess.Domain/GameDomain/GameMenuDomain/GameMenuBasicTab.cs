using InfinityWorldChess.PlayerDomain;
using InfinityWorldChess.RoleDomain;
using InfinityWorldChess.RoleDomain.Components;
using UnityEngine;

namespace InfinityWorldChess.GameDomain.GameMenuDomain
{
    public class GameMenuBasicTab : MonoBehaviour
    {
        [SerializeField] private NameEditor NameEditor;
        [SerializeField] private BodyPartEditor BodyPartEditor;
        [SerializeField] private BirthEditor BirthEditor;
        [SerializeField] private AvatarEditor AvatarEditor;

        private GameMenuTabItem _tabItem;
        private void Awake()
        {
            _tabItem ??= new GameMenuTabItem(nameof(GameMenuBasicTab), gameObject, Refresh);
        }


        private void Refresh()
        {
            PlayerGameContext player = GameScope.Instance.Player;

            NameEditor.Bind(player.Role.Basic);
            BodyPartEditor.Bind(player.Role.BodyPart);
            BirthEditor.Bind(player.Role.Basic);
            AvatarEditor.Bind(player.Role.Basic);
        }
    }
}