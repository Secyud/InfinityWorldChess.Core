using Secyud.Ugf.NotificationComponents;
using UnityEngine;

namespace InfinityWorldChess.MessageDomain
{
    [RequireComponent(typeof(Canvas))]
    public class MessageCanvas:MonoBehaviour
    {
        [SerializeField] private NotificationContent NotificationContent;

        public NotificationContent Notification => NotificationContent;

    }
}