using System;
using UnityEngine.UI;

namespace Schmiggolas.EasyNotifications
{
    public class NotificationSettings
    {
        public float displayDurationInSeconds;
        public string text;
        public Image icon;
        public ButtonSettings[] buttons;

        public NotificationSettings(float displayDurationInSeconds, string text, Image icon = null, ButtonSettings[] buttons = null)
        {
            this.displayDurationInSeconds = displayDurationInSeconds;
            this.text = text;
            this.icon = icon;
            this.buttons = buttons ?? Array.Empty<ButtonSettings>();
        }
    }
}