using System;
using InfinityWorldChess.BuffDomain;
using Secyud.Ugf.DataManager;

namespace InfinityWorldChess.ManufacturingFunctions
{
    public class ChangeAttachProperty
    {
        [field: S] public int Living { get; set; }
        [field: S] public int Kiling { get; set; }
        [field: S] public int Nimble { get; set; }
        [field: S] public int Defend { get; set; }

        protected void Change(IAttachProperty target)
        {
            target.Living = Math.Clamp(target.Living + Living, 0, 255);
            target.Kiling = Math.Clamp(target.Kiling + Kiling, 0, 255);
            target.Nimble = Math.Clamp(target.Nimble + Nimble, 0, 255);
            target.Defend = Math.Clamp(target.Defend + Defend, 0, 255);
        }
    }
}