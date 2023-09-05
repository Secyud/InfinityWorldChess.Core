using InfinityWorldChess.GameDomain;
using InfinityWorldChess.RoleDomain;
using InfinityWorldChess.Ugf;
using Secyud.Ugf;
using Secyud.Ugf.DataManager;
using Secyud.Ugf.HexMap;
using UnityEngine;

namespace InfinityWorldChess.SkillDomain
{
    public class PassiveSkill:IPassiveSkill
    {
        [field: S] public string ShowDescription { get; set; }
        [field: S] public string ShowName { get; set;}
        [field: S] public IObjectAccessor<Sprite> ShowIcon { get;set; }
        [field: S] public byte Score { get; set; }
        [field: S] public IPassiveSkillEffect Effect { get; set; }
        [field: S] public IObjectAccessor<HexUnitPlay> UnitPlay { get; set; }

        public int SaveIndex { get; set; }
        
        public void Equip(Role role)
        {
            Effect.Equip(this,role);
        }

        public void UnEquip(Role role)
        {
            Effect.UnEquip(this,role);
        }

        public void SetContent(Transform transform)
        {
            transform.AddSimpleShown(this);

            if (GameScope.Instance.Player.PlayerSetting?.WuXueQiCai == true)
            {
                SetHideContent(transform);
            }
        }

        protected virtual void SetHideContent(Transform transform)
        {
            transform.AddParagraph($"效果：{Effect.ShowDescription}。");
        }
        
        public byte Living { get; set; }
        public byte Kiling { get; set; }
        public byte Nimble { get; set; }
        public byte Defend { get; set; }
    }
}