using Secyud.Ugf;
using Secyud.Ugf.DataManager;
using UnityEngine;

namespace InfinityWorldChess.Ugf
{
    /// <summary>
    /// 引用资源，引用缓存
    /// 这种资源会留下一份引用，以便使用模板，模板内可能会存放描述数据。
    /// </summary>
    /// <typeparam name="TObject"></typeparam>
    public class AccessorWithTemplate<TObject>
    {
        [field: S] public IObjectAccessor<TObject> Accessor { get; set; }

        protected TObject Template => _template ??= Accessor.Value;
        private TObject _template;

        public virtual void SetContent(Transform transform)
        {
            Template.TrySetContent(transform);
        }
    }
}