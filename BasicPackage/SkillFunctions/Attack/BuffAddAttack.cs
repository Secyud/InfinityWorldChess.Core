using System;
using InfinityWorldChess.BattleDomain;
using InfinityWorldChess.SkillFunctions.Effect;
using Secyud.Ugf;
using Secyud.Ugf.DataManager;

namespace InfinityWorldChess.SkillFunctions.Attack
{
    public class BuffAddAttack : BasicAttack
    {
        [field: S] public Guid BuffTypeId { get; set; }
        [field: S] public string BuffName { get; set; }
        private object _template;

        private object Template => _template
            ??= U.Tm.ConstructFromResource(BuffTypeId, BuffName);

        public override string Description
        {
            get
            {
                if (Template is IHasDescription description)
                {
                    return base.Description + description.Description;
                }
                else
                {
                    return base.Description;
                }
            }
        }


        protected override void PostSkill(
            BattleRole battleChess, BattleCell releasePosition)
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