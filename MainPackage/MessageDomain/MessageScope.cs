using InfinityWorldChess.GlobalDomain;
using Secyud.Ugf;
using Secyud.Ugf.AssetComponents;
using Secyud.Ugf.BasicComponents;
using Secyud.Ugf.DependencyInjection;
using UnityEngine;

namespace InfinityWorldChess.MessageDomain
{
    [Registry(DependScope = typeof(GlobalScope))]
    public class MessageScope : DependencyScopeProvider
    {
        private readonly IMonoContainer<MessageCanvas> _messageCanvas;
        public MessageCanvas MessageCanvas => _messageCanvas.Value;

        public static MessageScope Instance { get; private set; }

        public MessageScope(IwcAssets assets)
        {
            _messageCanvas = MonoContainer<MessageCanvas>.Create(assets, onCanvas: false);
        }

        public override void OnInitialize()
        {
            Instance = this;
            _messageCanvas.Create();
            var canvas = _messageCanvas.Value.GetComponent<Canvas>();
            canvas.worldCamera = U.Camera;
            canvas.planeDistance = 935.3075f;
        }

        public override void Dispose()
        {
            Instance = null;
            _messageCanvas.Destroy();
        }

        public void AddMessage(string str)
        {
            var notification = MessageCanvas.Notification.CreateNew();

            var text = notification.GetComponentInChildren<SText>();

            text.text = str;

            notification.OnInitialize(MessageCanvas.Notification);
        }
    }
}