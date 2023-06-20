#region

using Secyud.Ugf;
using Secyud.Ugf.BasicComponents;
using Secyud.Ugf.FunctionalComponents;
using UnityEngine;
using UnityEngine.Events;

#endregion

namespace InfinityWorldChess.Ugf
{
    public class ImageCell : MonoBehaviour
    {
        public SImage Icon;

        public virtual void OnInitialize(ICanBeShown item)
        {
            Icon.Sprite = item?.ShowIcon?.Value;
        }

        public void SetFloating(IHasContent content, GameObject o = null)
        {
            AddHoverAction(
                content is null ? null : () => { content.CreateFloating(); },
                o ? o : gameObject
            );
        }


        public void AddHoverAction(UnityAction action, GameObject o)
        {
            Hoverable hoverable = o.GetOrAddComponent<Hoverable>();
            hoverable.OnHover.RemoveAllListeners();
            if (action is not null)
                hoverable.OnHover.AddListener(action);
        }
    }
}