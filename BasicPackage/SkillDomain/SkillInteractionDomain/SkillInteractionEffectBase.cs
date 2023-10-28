#region

using InfinityWorldChess.BattleDomain;
using Secyud.Ugf.HexMap;

#endregion

namespace InfinityWorldChess.SkillDomain.SkillInteractionDomain
{
    public abstract class SkillInteractionEffectBase : IActiveSkillEffect
    {
        // ReSharper disable once InconsistentNaming
        protected const string p = "\r\n · ";
        
        protected ISkillTarget Targets { get; set; }
        protected abstract ISkillTargetInRange TargetGetter { get; }
        public abstract string Description { get; }

        protected IActiveSkill Skill { get; set; }
        public void Cast(BattleRole role, HexCell releasePosition, ISkillRange range,IActiveSkill skill)
        {
            Skill = skill;
            Cast(role, releasePosition, range);
        }

        public void Cast(BattleRole role, HexCell releasePosition, ISkillRange range)
        {
            Targets = TargetGetter.GetTargetInRange(role, range);
            PreSkill(role, releasePosition);
            foreach (BattleRole enemy in Targets.Value)
            {
                SkillInteraction interaction =
                    SkillInteraction.Get(role, enemy);
                PreInteraction(interaction);
                interaction.BeforeHit();
                OnInteraction(interaction);
                interaction.AfterHit();
                PostInteraction(interaction);
            }

            PostSkill(role, releasePosition);
            Targets = null;
            Skill = null;
        }

        protected virtual void OnInteraction(SkillInteraction interaction)
        {
        }

        protected virtual void PreInteraction(SkillInteraction interaction)
        {
        }

        protected virtual void PreSkill(BattleRole battleChess, HexCell releasePosition)
        {
        }

        protected virtual void PostSkill(BattleRole battleChess, HexCell releasePosition)
        {
        }

        protected virtual void PostInteraction(SkillInteraction interaction)
        {
        }
    }
}