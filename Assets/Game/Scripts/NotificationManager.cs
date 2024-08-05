using System.Collections.Generic;
using Game.Scripts.Utilities;
using UnityEngine;

namespace Game.Scripts
{
    public class NotificationManager : Singleton<NotificationManager>
    {
        private List<Notification> _notifications = new List<Notification>();
        private float _notificationDuration = 2.0f;
        private Vector2 _notificationOffset = new Vector2(10, 10);
        private float _notificationFadeTime = 0.5f;
        
        private GUIStyle _guiStyle;

        protected override void Awake()
        {
            base.Awake();
            _guiStyle = new GUIStyle
            {
                fontSize = 24,
                normal =
                {
                    textColor = Color.white
                }
            };
        }

        private void Update()
        {
            UpdateNotifications();
        }

        private void UpdateNotifications()
        {
            for (int i = _notifications.Count - 1; i >= 0; i--)
            {
                _notifications[i].TimeLeft -= Time.deltaTime;
                if (_notifications[i].TimeLeft <= 0)
                {
                    _notifications.RemoveAt(i);
                }
            }
        }

        private void OnGUI()
        {
            for (var i = 0; i < _notifications.Count; i++)
            {
                var alpha = Mathf.Clamp01(_notifications[i].TimeLeft / _notificationFadeTime);
                GUI.color = new Color(1, 1, 1, alpha);
                var position = new Vector2(Screen.width - 400 - _notificationOffset.x, _notificationOffset.y + (i * 60));
                GUI.Label(new Rect(position, new Vector2(400, 40)), _notifications[i].Message, _guiStyle);
            }
            GUI.color = Color.white;
        }

        public void AddNotification(string message)
        {
            _notifications.Add(new Notification(message, _notificationDuration));
        }

        private class Notification
        {
            public string Message { get; private set; }
            public float TimeLeft { get; set; }

            public Notification(string message, float timeLeft)
            {
                Message = message;
                TimeLeft = timeLeft;
            }
        }
    }
    
}
