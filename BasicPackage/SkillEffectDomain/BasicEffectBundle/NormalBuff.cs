using InfinityWorldChess.BattleDomain;
using InfinityWorldChess.BuffDomain;
using Secyud.Ugf;
using Secyud.Ugf.DataManager;

namespace InfinityWorldChess.SkillEffectDomain.BasicEffectBundle
{
    /// <summary>
    /// no recorder buff, it will not remove from character.
    /// </summary>
    public class NormalBuff : IBuff<BattleRole>, IHasDescription
    {
        [field: S] public int Id { get; set; }
        [field: S] public IBuffEffect BuffEffect { get; set; }
        [S] private string _description;

        public string ShowDescription => _description
                                         + BuffEffect?.ShowDescription;

        public void Install(BattleRole target)
        {
            BuffEffect?.Install(target, this);
        }

        public void UnInstall(BattleRole target)
        {
            BuffEffect?.UnInstall(target, this);
        }

        public void Overlay(IBuff<BattleRole> finishBuff)
        {
            if (finishBuff is not RecorderBuff buff)
            {
                return;
            }

            BuffEffect?.Overlay(buff.BuffEffect, this);
        }
    }
}