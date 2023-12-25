using System;
using InfinityWorldChess.BuffDomain;
using Secyud.Ugf.DataManager;

namespace InfinityWorldChess.ManufacturingFunctions
{
    public class ChangeAttachProperty
    {
        [field: S] public byte Living { get; set; }
        [field: S] public byte Kiling { get; set; }
        [field: S] public byte Nimble { get; set; }
        [field: S] public byte Defend { get; set; }

        protected void Change(IAttachProperty target)
        {
            target.Living = (byte)Math.Clamp(target.Living+ Living,0,255) ;
            target.Kiling = (byte)Math.Clamp(target.Kiling+ Kiling,0,255);
            target.Nimble = (byte)Math.Clamp(target.Nimble+ Nimble,0,255);
            target.Defend = (byte)Math.Clamp(target.Defend+ Defend,0,255);
        }
    }
}