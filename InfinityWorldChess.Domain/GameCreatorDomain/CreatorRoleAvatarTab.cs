using InfinityWorldChess.RoleDomain;
using UnityEngine;

namespace InfinityWorldChess.GameCreatorDomain
{
    public class CreatorRoleAvatarTab: MonoBehaviour
    {
        [SerializeField] private AvatarEditor AvatarEditor;

        private CreatorTabItem _tabItem;
        private void Awake()
        {
            _tabItem ??= new CreatorTabItem(nameof(CreatorRoleAvatarTab), gameObject);
        }

        private void Start()
        {
            Role role = GameCreatorScope.Instance.Role;
            
            AvatarEditor.Bind(role.Basic);
        }
    }
}