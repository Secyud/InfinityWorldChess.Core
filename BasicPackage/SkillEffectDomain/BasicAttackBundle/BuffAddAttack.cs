using System;
using InfinityWorldChess.BattleDomain;
using InfinityWorldChess.SkillEffectDomain.BasicEffectBundle;
using Secyud.Ugf;
using Secyud.Ugf.DataManager;
using Secyud.Ugf.HexMap;

namespace InfinityWorldChess.SkillEffectDomain.BasicAttackBundle
{
    public class BuffAddAttack : BasicAttack
    {
        [field: S] public Guid BuffTypeId { get; set; }
        [field: S] public string BuffName { get; set; }
        private object _template;

        private object Template => _template
            ??= U.Tm.ConstructFromResource(BuffTypeId, BuffName);

        public override string ShowDescription
        {
            get
            {
                if (Template is IHasDescription description)
                {
                    return base.ShowDescription + description.ShowDescription;
                }
                else
                {
                    return base.ShowDescription;
                }
            }
        }


        protected override void PostSkill(
            BattleRole battleChess, HexCell releasePosition)
        {
            base.PostSkill(battleChess, releasePosition);

            object o = U.Tm.ConstructFromResource(BuffTypeId, BuffName);
            if (o is SkillBuffBase buff)
            {
                buff.SetSkill(Skill);
                battleChess.Buff.Install(buff);
            }
        }
    }
}