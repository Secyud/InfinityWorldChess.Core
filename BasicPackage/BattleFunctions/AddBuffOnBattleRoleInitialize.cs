using InfinityWorldChess.BattleDomain;
using InfinityWorldChess.BuffDomain;
using InfinityWorldChess.SkillDomain;
using InfinityWorldChess.SkillFunctions;
using InfinityWorldChess.Ugf;
using Secyud.Ugf;
using Secyud.Ugf.DataManager;
using UnityEngine;

namespace InfinityWorldChess.BattleFunctions
{
    public class AddBuffOnBattleRoleInitialize:IOnBattleRoleInitializeP
    {
        [field: S] public IObjectAccessor<SkillBuff> Buff { get; set; }
        private SkillBuff _template;
        private SkillBuff Template => _template ??= Buff.Value;

        public IBuffProperty Property { get; set; }
        
        public void OnBattleInitialize(BattleRole chess)
        { 
            SkillBuff buff = Buff.Value;
            buff.SetProperty(Property,chess);
            chess.Buff.Install(buff);
        }

        public void SetContent(Transform transform)
        {
            transform.AddParagraph($"战斗开始时获得状态：{Template.Name}。");
            Template.SetContent(transform);
        }
    }
}