#region

using InfinityWorldChess.BattleDomain;
using InfinityWorldChess.RoleDomain;
using InfinityWorldChess.SkillDomain;

#endregion

namespace InfinityWorldChess.Ugf
{
    public static class BpSkillExtension
    {
        public static AttackRecordBuff GetOrAddAttack(this SkillInteraction interaction)
        {
            return interaction.TypeBuff.GetOrInstall<AttackRecordBuff>();
        }

        public static AttackRecordBuff GetAttack(this SkillInteraction interaction)
        {
            return interaction.TypeBuff.Get<AttackRecordBuff>();
        }

        public static TreatRecordBuff GetOrAddTreat(this SkillInteraction interaction)
        {
            return interaction.TypeBuff.GetOrInstall<TreatRecordBuff>();
        }

        public static TreatRecordBuff GetTreat(this SkillInteraction interaction)
        {
            return interaction.TypeBuff.Get<TreatRecordBuff>();
        }

        public static bool FitWeapon(this ICoreSkill coreSkill, BattleRole chess)
        {
            byte tc = chess.Role.Equipment[BodyType.Kiling]?.TypeCode ?? 0;
            return ((tc ^ coreSkill.ConditionCode) & coreSkill.ConditionMask) == 0;
        }
    }
}