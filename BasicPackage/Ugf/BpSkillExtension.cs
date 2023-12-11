#region

using InfinityWorldChess.BattleBuffFunction;
using InfinityWorldChess.BattleDomain;
using InfinityWorldChess.BattleInteractionDomain;
using InfinityWorldChess.SkillDomain;

#endregion

namespace InfinityWorldChess.Ugf
{
    public static class BpSkillExtension
    {
        public static void TryAttach(this IBattleUnitBuff buff, object attached)
        {
            if (attached is IBuffAttached buffAttached)
            {
                buffAttached.Buff = buff;
            }
        }
        
        public static AttackRecordProperty GetOrAddAttack(this BattleInteraction interaction)
        {
            return interaction.Properties.GetOrCreate<AttackRecordProperty>();
        }

        public static AttackRecordProperty GetAttack(this BattleInteraction interaction)
        {
            return interaction.Properties.Get<AttackRecordProperty>();
        }

        public static TreatRecordProperty GetOrAddTreat(this BattleInteraction interaction)
        {
            return interaction.Properties.GetOrCreate<TreatRecordProperty>();
        }

        public static TreatRecordProperty GetTreat(this BattleInteraction interaction)
        {
            return interaction.Properties.Get<TreatRecordProperty>();
        }

        public static bool FitWeapon(this IActiveSkill coreSkill, BattleUnit chess)
        {
            byte tc = chess.Role.Equipment.Get()?.TypeCode ?? 0;
            return ((tc ^ coreSkill.ConditionCode) & coreSkill.ConditionMask) == 0;
        }
    }
}