using Secyud.Ugf;
using Secyud.Ugf.DataManager;
using UnityEngine;

namespace InfinityWorldChess.Ugf
{
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