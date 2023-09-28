using InfinityWorldChess.BattleDomain;
using InfinityWorldChess.BuffDomain;
using InfinityWorldChess.SkillDomain;
using Secyud.Ugf;
using Secyud.Ugf.DataManager;

namespace InfinityWorldChess.SkillEffectDomain.BasicEffectBundle
{
    public abstract class SkillBuffBase: IBuff<BattleRole>, IHasDescription,ISkillBuff
    {
        [field: S] public int Id { get; set; }
        [field: S] public IBuffEffect BuffEffect { get; set; }
        [S] private string _description;

        public virtual string ShowDescription => _description
                                         + BuffEffect?.ShowDescription;
        public virtual void Install(BattleRole target)
        {
            BuffEffect?.Install(target, this);
        }

        public virtual void UnInstall(BattleRole target)
        {
            BuffEffect?.UnInstall(target, this);
        }

        public virtual void Overlay(IBuff<BattleRole> finishBuff)
        {
            if (finishBuff is not SkillBuffBase buff)
            {
                return;
            }

            BuffEffect?.Overlay(buff.BuffEffect, this);
        }

        public virtual void SetSkill(IActiveSkill skill)
        {
            BuffEffect?.SetSkill(skill);
        }

    }
}