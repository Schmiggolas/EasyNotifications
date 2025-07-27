using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Schmiggolas.EasyNotifications
{
    public class Notification : MonoBehaviour
    {
        private const string CloseAnimationTriggerName = "Close";
        [SerializeField] private TMP_Text text;
        [SerializeField] private GameObject iconContainer;
        [SerializeField] private Image icon;
        [SerializeField] private GameObject buttonContainer;
        [SerializeField] private Transform buttonSpawnTarget;
        [SerializeField] private Transform buttonSpacerPrefab;
        [SerializeField] private GameObject buttonPrefab;
        [SerializeField] private Image closeButtonFillIcon;
        [SerializeField] private Animator animator;

        private Coroutine _autoCloseCoroutine;
        private Action<Notification> _notificationClosedCallback;

        public void Initialize(NotificationSettings settings, Action<Notification> notificationClosedCallback)
        {
            _notificationClosedCallback = notificationClosedCallback;
            text.text = settings.text;

            if (settings.icon != null)
            {
                iconContainer.SetActive(true);
                icon.sprite = settings.icon.sprite;
            }

            // we space out the buttons with a spacer prefab
            // one spacer before the first button and one after each button
            // this way the buttons are evenly spaced
            // kinda overkill for one button, since we can just center it,
            // but it's needed for multiple buttons
            if (settings.buttons.Length > 0)
            {
                buttonContainer.SetActive(true);
                Instantiate(buttonSpacerPrefab, buttonSpawnTarget);
            }

            foreach (var buttonSetting in settings.buttons)
            {
                var buttonInstance = Instantiate(buttonPrefab, buttonSpawnTarget);
                if (buttonInstance.TryGetComponent<Button>(out var buttonComponent))
                {
                    buttonComponent.GetComponentInChildren<TMP_Text>().text = buttonSetting.text;
                    buttonComponent.onClick.AddListener(() =>
                    {
                        buttonSetting.onClick?.Invoke();
                        if (buttonSetting.closeOnClick)
                        {
                            Close();
                        }
                    });
                }
                Instantiate(buttonSpacerPrefab, buttonSpawnTarget);
            }

            // if the display duration is greater than 0, start the auto-close coroutine
            // if it's 0, then we don't want to auto-close
            if (settings.displayDurationInSeconds > 0)
            {
                _autoCloseCoroutine = StartCoroutine(AutoClose(settings.displayDurationInSeconds));
            }
            else
            {
                closeButtonFillIcon.gameObject.SetActive(false);
            }
        }

        public void Close()
        {
            if (_autoCloseCoroutine != null)
            {
                StopCoroutine(_autoCloseCoroutine);
            }
            animator.SetTrigger(CloseAnimationTriggerName);
        }

        public void OnCloseAnimationComplete()
        {
            // This method is called by the animator when the close animation is complete
            _notificationClosedCallback?.Invoke(this);
        }

        private IEnumerator AutoClose(float duration)
        {
            var elapsedTime = 0f;
            while (elapsedTime < duration)
            {
                elapsedTime += Time.deltaTime;
                closeButtonFillIcon.fillAmount = 1 - (elapsedTime / duration);
                yield return null;
            }
            Close();
        }
    }
}