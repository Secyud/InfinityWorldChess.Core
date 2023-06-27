using InfinityWorldChess.RoleDomain;
using InfinityWorldChess.SkillDomain;
using InfinityWorldChess.Ugf;
using Secyud.Ugf;
using Secyud.Ugf.DataManager;
using Secyud.Ugf.HexMap;
using UnityEngine;

namespace InfinityWorldChess.BasicBundle.PassiveSkills
{
    public class PassiveSkillTemplate : DataObject, IPassiveSkill
    {
        [field: S(ID = 0, DataType = DataType.Initialed)]
        public string ShowDescription { get; set; }

        [field: S(ID = 1, DataType = DataType.Initialed)]
        public IObjectAccessor<Sprite> ShowIcon { get; set; }

        [field: S(ID = 2, DataType = DataType.Initialed)]
        public IObjectAccessor<HexUnitPlay> UnitPlay { get; set; }

        [field: S(ID = 0)] public byte Score { get; set; }
        [field: S(ID = 1)] public short Yin { get; set; }
        [field: S(ID = 2)] public short Yang { get; set; }
        [field: S(ID = 3)] public byte Living { get; set; }
        [field: S(ID = 4)] public byte Kiling { get; set; }
        [field: S(ID = 5)] public byte Nimble { get; set; }
        [field: S(ID = 6)] public byte Defend { get; set; }
        public string ShowName => ObjectName;
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