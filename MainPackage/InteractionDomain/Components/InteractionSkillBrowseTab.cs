using InfinityWorldChess.RoleDomain;
using InfinityWorldChess.SkillDomain;
using Secyud.Ugf.TabComponents;
using UnityEngine;

namespace InfinityWorldChess.InteractionDomain
{
    public class InteractionSkillBrowseTab : TabPanel
    {
        [SerializeField] private SkillBrowseTabs SkillBrowseTabs;

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
            SkillBrowseTabs.OnInitialize(role);
        }
    }
}