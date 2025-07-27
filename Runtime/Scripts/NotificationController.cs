using System.Collections.Concurrent;
using System.Collections.Generic;
using UnityEngine;

namespace Schmiggolas.EasyNotifications
{
    public class NotificationController : MonoBehaviour
    {
        private static readonly ConcurrentQueue<NotificationSettings> _notificationQueue = new();
        [SerializeField] private GameObject notificationPrefab;
        [SerializeField] private Transform notificationContainer;
        [SerializeField] private int maxNotificationCount;

        private List<Notification> _activeNotifications;

        private void Awake()
        {
            _activeNotifications = new List<Notification>();
        }

        private void Update()
        {
            while (CanHandleNotification() && _notificationQueue.TryDequeue(out var notification))
            {
                var notificationInstance = Instantiate(notificationPrefab, notificationContainer);
                if (notificationInstance.TryGetComponent<Notification>(out var notificationComponent))
                {
                    notificationComponent.Initialize(notification, OnNotificationClosed);
                    _activeNotifications.Add(notificationComponent);
                }
                else
                {
                    Debug.LogError($"[{nameof(NotificationController)}] Notification prefab does not have a Notification component.", notificationInstance);
                }
            }
        }
        
        private void OnNotificationClosed(Notification notification)
        {
            _activeNotifications.Remove(notification);
            Destroy(notification.gameObject);
        }

        public bool CanHandleNotification()
        {
            if (maxNotificationCount == 0)
            {
                return true;
            }
            return _activeNotifications.Count < maxNotificationCount;
        }

        public static void EnqueueNotification(NotificationSettings notification)
        {
            _notificationQueue.Enqueue(notification);
        }
    }
}
