using System.Collections.Generic;
using InfinityWorldChess.RoleDomain;
using Secyud.Ugf;
using Secyud.Ugf.BasicComponents;
using Secyud.Ugf.TabComponents;
using Secyud.Ugf.TableComponents.ButtonComponents;
using UnityEngine;

namespace InfinityWorldChess.InteractionDomain
{
    public class InteractionBasicTab : TabPanel
    {
        [SerializeField] private AvatarEditor AvatarEditor;
        [SerializeField] private SButtonGroup ButtonGroup;

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
            List<ButtonDescriptor<Role>> buttons = 
                U.Get<InteractionButtons>().Items;
            ButtonGroup.OnInitialize(role, buttons);
            AvatarEditor.OnInitialize(role.Basic);
        }
    }
}