using InfinityWorldChess.BattleDomain;
using InfinityWorldChess.BuffDomain;
using InfinityWorldChess.SkillDomain;
using InfinityWorldChess.Ugf;
using Secyud.Ugf;
using Secyud.Ugf.DataManager;
using UnityEngine;

namespace InfinityWorldChess.SkillFunctions
{
    public class BuffWithContent
    {
        [field: S] public IObjectAccessor<SkillBuff> Buff { get; set; }

        private SkillBuff _buffTemplate;
        protected SkillBuff BuffTemplate => _buffTemplate ??= Buff?.Value;

        public void InstallBuff(BattleRole battleChess, IBuffProperty property)
        {
            SkillBuff buff = Buff?.Value;
            if (buff is null) return;
            buff.SetProperty(property);
            battleChess.Buff.Install(buff);
        }

        public virtual void SetContent(Transform transform)
        {
            if (BuffTemplate is not null)
            {
                transform.AddParagraph($"为目标添加状态{BuffTemplate.Name}。");
                BuffTemplate.SetContent(transform);
            }
        }
    }
}