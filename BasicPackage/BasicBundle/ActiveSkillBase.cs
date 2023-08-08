using System.Ugf;
using InfinityWorldChess.BattleDomain;
using InfinityWorldChess.GameDomain;
using InfinityWorldChess.SkillDomain;
using InfinityWorldChess.Ugf;
using Secyud.Ugf;
using Secyud.Ugf.DataManager;
using Secyud.Ugf.HexMap;
using UnityEngine;

namespace InfinityWorldChess.BasicBundle
{
    public abstract class ActiveSkillBase : DataObject, IActiveSkill
    {
        [field: S(ID = 0, DataType = DataType.Initialed)]
        public string ShowDescription { get; set; }
        [field: S(ID = 1, DataType = DataType.Initialed)]
        public IObjectAccessor<HexUnitPlay> UnitPlay { get; set; }
        [field: S(ID = 2, DataType = DataType.Initialed)]
        public IObjectAccessor<Sprite> ShowIcon { get; set; }

        [field: S(ID = 0)] public byte Score { get; set; }
        [field: S(ID = 1)] public byte ExecutionConsume { get; set; }
        [field: S(ID = 2)] public ISkillCastPosition Position { get; set; }
        [field: S(ID = 3)] public ISkillCastResult Range { get; set; }
        [field: S(ID = 4)] public float EnergyConsume { get; set; }
        
        public string ShowName => ObjectName;
        public virtual SkillTargetType TargetType => SkillTargetType.Enemy;
        public virtual bool Damage => true;
        protected abstract string HDescription { get; }
        public int SaveIndex { get; set; }

        protected virtual string HideDescription
        {
            get
            {
                string d = HDescription;
                return (d.IsNullOrEmpty() ? "" : $" · {d}") +
                       $"\r\n · 行动力消耗: {ExecutionConsume}\r\n · 内力消耗: {EnergyConsume}";
            }
        }

        public virtual void Release()
        {
        }

        public virtual void SetContent(Transform transform)
        {
            transform.AddSimpleShown(this);
            if (GameScope.Instance.Player.PlayerSetting?.WuXueQiCai == true)
            {
                transform.AddTitle3("技能信息");
                transform.AddParagraph(HideDescription);
                {
                    if (Position is IHasContent content)
                        content.SetContent(transform);
                }
                {
                    if (Range is IHasContent content)
                        content.SetContent(transform);
                }
            }
        }

        public virtual string CheckCastCondition(BattleRole chess)
        {
            if (chess.ExecutionValue < ExecutionConsume)
                return "行动力不足，无法释放技能。";

            if (EnergyConsume > chess.EnergyValue)
                return "内力不足，无法释放技能。";

            return null;
        }

        public virtual ISkillRange GetCastPositionRange(BattleRole role)
        {
            return Position.GetCastPositionRange(role) ;
        }

        public virtual ISkillRange GetCastResultRange(BattleRole role, HexCell castPosition)
        {
            return Range.GetCastResultRange(role,castPosition) ;
        }

        public virtual void Cast(BattleRole role, HexCell releasePosition)
        {
            role.EnergyValue -= EnergyConsume;
            role.ExecutionValue -= ExecutionConsume;
        }
    }
}