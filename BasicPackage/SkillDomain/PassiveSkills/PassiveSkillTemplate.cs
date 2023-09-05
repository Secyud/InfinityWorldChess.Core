using InfinityWorldChess.RoleDomain;
using InfinityWorldChess.SkillDomain;
using InfinityWorldChess.Ugf;
using Secyud.Ugf;
using Secyud.Ugf.DataManager;
using Secyud.Ugf.HexMap;
using UnityEngine;

namespace InfinityWorldChess.BasicBundle.PassiveSkills
{
    public class PassiveSkillTemplate :  IPassiveSkill
    {
        [field: S] public string ShowName  { get; set; }
        [field: S] public string ShowDescription { get; set; }
        [field: S] public IObjectAccessor<Sprite> ShowIcon { get; set; }
        [field: S] public IObjectAccessor<HexUnitPlay> UnitPlay { get; set; }
        [field: S] public byte Score { get; set; }
        [field: S] public short Yin { get; set; }
        [field: S] public short Yang { get; set; }
        [field: S] public byte Living { get; set; }
        [field: S] public byte Kiling { get; set; }
        [field: S] public byte Nimble { get; set; }
        [field: S] public byte Defend { get; set; }
        public virtual string HideDescription => "没有特殊效果。";
        public int SaveIndex { get; set; }

        public virtual void Equip(Role role)
        {
        }

        public virtual void UnEquip(Role role)
        {
        }

        public virtual void SetContent(Transform transform)
        {
            transform.AddSimpleShown(this);
        }
    }
}