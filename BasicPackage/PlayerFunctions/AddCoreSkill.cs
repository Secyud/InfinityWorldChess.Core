using InfinityWorldChess.FunctionDomain;
using InfinityWorldChess.GameDomain;
using InfinityWorldChess.MessageDomain;
using InfinityWorldChess.SkillDomain;
using InfinityWorldChess.Ugf;
using Secyud.Ugf;
using UnityEngine;

namespace InfinityWorldChess.PlayerFunctions
{
    public class AddCoreSkill : AccessorWithTemplate<ICoreSkill>, IActionable, IHasContent
    {
        public void Invoke()
        {
            var item = Accessor.Value;
            MessageScope.Instance.AddMessage($"习得{item.Name}");
            GameScope.Instance.Player.Role.CoreSkill.TryAddLearnedSkill(item);
        }

        public override void SetContent(Transform transform)
        {
            transform.AddParagraph($"可习得招式{Template.Name}。");
            base.SetContent(transform);
        }
    }
}