using System;
using InfinityWorldChess.FunctionDomain;
using InfinityWorldChess.RoleDomain;
using Secyud.Ugf.DataManager;

namespace InfinityWorldChess.RoleFunctions
{
    public class RoleChangeRealProperty:IActionable<Role>
    {
        [field:S] public int Living { get; set; }
        [field:S] public int Kiling { get; set; }
        [field:S] public int Nimble { get; set; }
        [field:S] public int Defend { get; set; }
        public void Invoke(Role target)
        {
            target.BodyPart.Living.ChangeRealValue(Living);
            target.BodyPart.Kiling.ChangeRealValue(Kiling);
            target.BodyPart.Nimble.ChangeRealValue(Nimble);
            target.BodyPart.Defend.ChangeRealValue(Defend);
        }
        
    }
}