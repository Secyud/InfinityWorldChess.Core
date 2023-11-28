using InfinityWorldChess.RoleDomain;
using Secyud.Ugf.TabComponents;
using UnityEngine;

namespace InfinityWorldChess.GameCreatorDomain
{
    public class CreatorRoleAvatarTab: TabPanel
    {
        [SerializeField] private AvatarEditor AvatarEditor;
        [SerializeField] private AvatarElementSliders AvatarSliders;

        private CreatorTabService _service;
        protected override TabService Service => _service;

        protected override void Awake()
        {
            _service = GameCreatorScope.Instance.Get<CreatorTabService>();
            base.Awake();
        }

        public override void RefreshTab()
        {
            Role role = GameCreatorScope.Instance.Context.Role;
            AvatarEditor.OnInitialize(role.Basic);
            AvatarSliders.RefreshMaxValue();
        }
    }
}