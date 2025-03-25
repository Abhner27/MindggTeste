using System.Collections.Generic;
using UnityEngine;

public partial class NotificationsManager : MonoBehaviour
{
    private const int MAX_SHOWN_NOTIFICATION_COUNT = 3;
    private const float NOTIFICATION_LIFETIME = 3f;

    private Queue<Notification> _notifications = new Queue<Notification>(MAX_SHOWN_NOTIFICATION_COUNT);

    [SerializeField]
    private NotificationDatas notificationDatas;

    [Header("Templates")]

    [SerializeField]
    private GameObject _paperTemplate;
    [SerializeField]
    private GameObject _woodTemplate;

    public delegate void CreateNewNotification(Notification notification);
    public event CreateNewNotification CreateNotificationEvent;

    public delegate void DeleteNotification();
    public event DeleteNotification DeleteNotificationEvent;

    public void EnqueueNewNotification(Notification notification)
    {
        Debug.Log(notification);

        _notifications.Enqueue(notification);

        CreateNotificationEvent?.Invoke(notification);

        //if the queue is bigger than  the max, dequeue that notification and delete it!
        if (_notifications.Count > MAX_SHOWN_NOTIFICATION_COUNT)
        {
            _notifications.Dequeue();
            DeleteNotificationEvent?.Invoke();
        }

        //Show notification
        ShowNotification(notification);
    }

    public async void ShowNotification(Notification notification)
    {
        //Set the template of the notification ON
        GameObject templateGO = GetTemplateGO(notification.Template);
        templateGO.SetActive(true);

        //Then Update its UI with the NotificationUI script
        NotificationUI notificationUI = templateGO.GetComponent<NotificationUI>();
        notificationUI.UpdateUI(notification);

        await CustomTimeManager.WaitForGameTime(NOTIFICATION_LIFETIME);
        templateGO.SetActive(false);
    }

    private GameObject GetTemplateGO(Notification.Templates template)
    {
        switch (template)
        {
            case Notification.Templates.Paper:
                return _paperTemplate;

            case Notification.Templates.Wood:
                return _woodTemplate;

            default:
                return null;
        }

    }
}