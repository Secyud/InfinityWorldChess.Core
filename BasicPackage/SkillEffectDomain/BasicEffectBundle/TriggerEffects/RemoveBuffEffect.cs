using InfinityWorldChess.BattleDomain;
using InfinityWorldChess.BuffDomain;
using InfinityWorldChess.SkillDomain;
using Secyud.Ugf.DataManager;

namespace InfinityWorldChess.SkillEffectDomain.BasicEffectBundle
{
    public class RemoveBuffEffect:ITriggerEffect
    {
        private IBuff<BattleRole> _buff;
        private BattleRole _target;
        [field:S] public int TriggerTime { get; set; }
        public string ShowDescription => $"剩余{TriggerTime}次。";
        public void Install(BattleRole target, IBuff<BattleRole> buff)
        {
            _target = target;
            _buff = buff;
        }

        public void UnInstall(BattleRole target, IBuff<BattleRole> buff)
        {
        }

        public void Overlay(IBuffEffect sameEffect, IBuff<BattleRole> buff)
        {
        }

        public void Active()
        {
            TriggerTime--;
            if (TriggerTime <= 0)
            {
                _target.Buff.Remove(_buff.Id);
            }
        }

        public void SetSkill(IActiveSkill skill)
        {
            
        }
    }
}