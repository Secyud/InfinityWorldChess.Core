using InfinityWorldChess.BattleDomain;
using InfinityWorldChess.SkillDomain;
using InfinityWorldChess.Ugf;
using Secyud.Ugf;
using Secyud.Ugf.DataManager;
using UnityEngine;

namespace InfinityWorldChess.SkillFunctions.InteractionLimits
{
    public class TargetBuffLimit: ILimitCondition, IHasContent
    {
        [field:S] public int BuffId { get; set; }
        [field:S] public string BuffName { get; set; }
        
        public bool CheckLimit(object sender)
        {
            BattleRole role = null;

            if (sender is SkillInteraction interaction)
            {
                role = interaction.TargetChess;
            }

            if (role is not null)
            {
                return role.Buff.ContainsKey(BuffId);
            }

            return false;
        }

        public void SetContent(Transform transform)
        {
            transform.AddParagraph($"如果目标角色拥有[{BuffName}]状态，则");
        }
    }
}