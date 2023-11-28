using System;
using InfinityWorldChess.BattleInteractionDomain;
using InfinityWorldChess.FunctionDomain;
using InfinityWorldChess.GlobalDomain;
using InfinityWorldChess.Ugf;
using Secyud.Ugf;
using Secyud.Ugf.DataManager;
using UnityEngine;

namespace InfinityWorldChess.BattleInteractionFunctions
{
    public class TreatInitSetting:IHasPriority,
        IActionable<BattleInteraction>,IHasContent
    {
        [field: S] public float Treat { get; set; }
        [field: S] public float TreatFixedValue { get; set; }
        [field: S] public float TreatFactor { get; set; } = 1f;
        [field: S] public float RecoverFactor { get; set; } = 1f;
        [field: S] public float RestrainFactor { get; set; }

        public int Priority => -0x10001;

        
        public void SetContent(Transform transform)
        {
            string str = "";

            if (Treat != 0)
            {
                str += "\r\n治疗: " + Treat;
            }
            
            if (TreatFixedValue != 0)
            {
                str += "\r\n额外治疗: " + TreatFixedValue;
            }

            if (Math.Abs(TreatFactor - 1) > IWCC.T)
            {
                str += "\r\n治疗倍率: " + TreatFactor;
            }

            if (Math.Abs(RecoverFactor - 1) > IWCC.T)
            {
                str += "\r\n额外治疗倍率: " + RecoverFactor;
            }

            if (RestrainFactor != 0)
            {
                str += "\r\n治疗衰减: " + RestrainFactor;
            }

            transform.AddParagraph(str);
        }

        public void Invoke(BattleInteraction interaction)
        {
            TreatRecordProperty record = interaction.GetOrAddTreat();
            record.Treat = Treat;
            record.TreatFixedValue = TreatFixedValue;
            record.TreatFactor = TreatFactor;
            record.RecoverFactor = RecoverFactor;
            record.RestrainFactor = RestrainFactor;
        }
    }
}