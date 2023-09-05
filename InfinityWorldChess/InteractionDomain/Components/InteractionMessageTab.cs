using InfinityWorldChess.RoleDomain;
using Secyud.Ugf.TabComponents;
using UnityEngine;

namespace InfinityWorldChess.InteractionDomain
{
    public class InteractionMessageTab : TabPanel
    {
        [SerializeField] private NameEditor NameEditor;
        [SerializeField] private GenderEditor GenderEditor;
        [SerializeField] private BirthEditor BirthEditor;

        private InteractionTabService _service; 
        protected override TabService Service => _service;

        protected override void Awake()
        {
            _service = InteractionScope.Instance.Get<InteractionTabService>();
            base.Awake();
        }

        public override void RefreshTab()
        {
            Role role = _service.InteractionRole;

            NameEditor.Bind(role.Basic);
            GenderEditor.Bind(role.Basic);
            BirthEditor.Bind(role.Basic);
        }
    }
}