using UnityEngine;

namespace Schmiggolas.EasyNotifications
{
    public class EasyNotificationsTester : MonoBehaviour
    {
        public void SpawnNotificationNoButtons()
        {
            var settings = new NotificationSettings(0, "Test Notification No Buttons");

            NotificationController.EnqueueNotification(settings);
        }
        public void SpawnNotificationNoButtonsWithTimeout()
        {
            var settings = new NotificationSettings(10, "Test Notification No Buttons");

            NotificationController.EnqueueNotification(settings);
        }

        public void SpawnNotificationWithButtons()
        {
            var settings = new NotificationSettings(0, "Test Notification With Buttons",null, new ButtonSettings[]{
                new("Button 1", () => Debug.Log("Button 1 clicked"), false),
                new("Button 2", () => Debug.Log("Button 2 clicked"), false),
                new("Close", () => Debug.Log("Button 3 clicked"), true) // closes the notification after clicking
            });

            NotificationController.EnqueueNotification(settings);
        }
    }
}
