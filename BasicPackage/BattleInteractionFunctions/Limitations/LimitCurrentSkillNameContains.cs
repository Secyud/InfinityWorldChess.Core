using InfinityWorldChess.BattleDomain;
using InfinityWorldChess.FunctionDomain;
using InfinityWorldChess.SkillDomain;
using InfinityWorldChess.Ugf;
using Secyud.Ugf;
using Secyud.Ugf.DataManager;
using UnityEngine;

namespace InfinityWorldChess.BattleInteractionFunctions
{
    /// <summary>
    /// effect run if current skill name contains the str NameContain
    /// </summary>
    public class LimitCurrentSkillNameContains : ILimitable, IHasContent
    {
        [field: S] public string NameContain { get; set; }

        public void SetContent(Transform transform)
        {
            transform.AddParagraph($"如果当前释放的技能名称中包含{NameContain}，则");
        }

        public bool CheckUseful()
        {
            CoreSkillContainer coreSkill =
                BattleScope.Instance.Get<CoreSkillActionService>().CoreSkill;

            if (coreSkill is not null &&
                coreSkill.CoreSkill.Name.Contains(NameContain))
            {
                return true;
            }

            FormSkillContainer formSkill =
                BattleScope.Instance.Get<FormSkillActionService>().FormSkill;

            if (formSkill is not null &&
                formSkill.FormSkill.Name.Contains(NameContain))
            {
                return true;
            }

            return false;
        }
    }
}