using InfinityWorldChess.RoleDomain;
using InfinityWorldChess.Ugf;
using Secyud.Ugf.TabComponents;
using Secyud.Ugf.TableComponents;
using UnityEngine;

namespace InfinityWorldChess.InteractionDomain
{
    public class InteractionBuffTab:TabPanel
    {
        [SerializeField] private Table BuffTable;
        [SerializeField] private BodyPartEditor BodyPartEditor;

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

            BuffTable.AutoSetTable(role.IdBuffs.GetVisibleBuff());
            BodyPartEditor.Bind(role.BodyPart);
        }
    }
}