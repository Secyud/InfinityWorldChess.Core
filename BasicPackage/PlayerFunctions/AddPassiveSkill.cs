using InfinityWorldChess.FunctionDomain;
using InfinityWorldChess.GameDomain;
using InfinityWorldChess.MessageDomain;
using InfinityWorldChess.SkillDomain;
using InfinityWorldChess.Ugf;
using Secyud.Ugf;
using UnityEngine;

namespace InfinityWorldChess.PlayerFunctions
{
    public class AddPassiveSkill:AccessorWithTemplate<IPassiveSkill>, IActionable ,IHasContent
    {
        public void Invoke()
        {
            var item = Accessor.Value;
            MessageScope.Instance.AddMessage($"习得{item.Name}");
            GameScope.Instance.Player.Role.PassiveSkill.TryAddLearnedSkill(item);
        }

        public override void SetContent(Transform transform)
        {
            transform.AddParagraph($"可习得内功{Template.Name}。");
            base.SetContent(transform);
        }
    }
}