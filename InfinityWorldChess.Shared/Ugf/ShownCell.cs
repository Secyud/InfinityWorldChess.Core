#region

using Secyud.Ugf;
using Secyud.Ugf.BasicComponents;
using Secyud.Ugf.FunctionalComponents;
using Secyud.Ugf.TableComponents;
using UnityEngine;

#endregion

namespace InfinityWorldChess.Ugf
{
    public class ShownCell : TableCell
    {
        [SerializeField] private SImage IconImage;
        [SerializeField] private SImage Background;
        [SerializeField] private SImage BorderImage;
        [SerializeField] private SImage SelectImage;
        [SerializeField] private SText ShowLabel;

        public SImage Icon => IconImage;
        public SImage Back => Background;
        public SImage Border => BorderImage;
        public SImage Select => SelectImage;
        public SText Label => ShowLabel;

        private IHasContent _withContent;

        public virtual void BindShowable(IShowable item)
        {
            if (IconImage)
                IconImage.Sprite = item?.ShowIcon?.Value;
            if (ShowLabel)
                ShowLabel.text = U.T[item?.ShowName];;
            if (item is IHasContent withContent)
            {
                _withContent = withContent;
                SDelayedHoverable sDelayedHoverable =
                    gameObject.GetOrAddComponent<SDelayedHoverable>();
                sDelayedHoverable.OnHoverTrig.RemoveAllListeners();
                sDelayedHoverable.OnHoverTrig.AddListener(CreateFloating);
            }
        }

        private void CreateFloating()
        {
            _withContent.CreateFloating();
        }


        //
        // public void SetFloating(IHasContent content, GameObject o = null)
        // {
        //     AddHoverAction(
        //         content is null ? null : () => { content.CreateFloating(); },
        //         o ? o : gameObject
        //     );
        // }
        //
        //
        // public void AddHoverAction(UnityAction action, GameObject o)
        // {
        //     SDelayedHoverable sDelayedHoverable = o.GetOrAddComponent<SDelayedHoverable>();
        //     sDelayedHoverable.OnHover.RemoveAllListeners();
        //     if (action is not null)
        //         sDelayedHoverable.OnHover.AddListener(action);
        // }
    }
}