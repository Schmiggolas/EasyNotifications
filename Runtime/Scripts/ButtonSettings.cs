using System;

namespace Schmiggolas.EasyNotifications
{
    public class ButtonSettings
    {
        public string text;
        public Action onClick;
        public bool closeOnClick;

        public ButtonSettings(string text, Action onClick, bool closeOnClick = true)
        {
            this.text = text;
            this.onClick = onClick;
            this.closeOnClick = closeOnClick;
        }
    }
}