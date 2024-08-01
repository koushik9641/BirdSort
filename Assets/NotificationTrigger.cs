using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NotificationTrigger : Singleton<NotificationTrigger>
{

    public int HourAfterNotification;
    public int MinutesAfterNotification;
    public string NotificationHeadingText;
    public string NotificationBodyText;
    public string ReturningNotificationtext;
    // Start is called before the first frame update
    void Start()
    {

        GleyNotifications.Initialize();

    }

    public void SendNotification()
    {

        GleyNotifications.SendNotification(NotificationHeadingText, NotificationBodyText, new System.TimeSpan(HourAfterNotification, MinutesAfterNotification, 0), "icon_0", "icon_1", ReturningNotificationtext);

    }




    /// <summary>
    /// The best way to schedule notifications is from OnApplicationFocus method
    /// when this is called user left your app
    /// when you trigger notifications when user is still in app, maybe your notification will be delivered when user is still inside the app and that is not good practice  
    /// </summary>
    /// <param name="focus"></param>
    private void OnApplicationFocus(bool focus)
    {

        if (focus == false)
        {
            //if user left your app schedule all your notifications
            GleyNotifications.SendNotification(NotificationHeadingText, NotificationBodyText, new System.TimeSpan(HourAfterNotification, MinutesAfterNotification, 0), "icon_0", "icon_1", ReturningNotificationtext);
        }
        else
        {
            //call initialize when user returns to your app to cancel all pending notifications
            GleyNotifications.Initialize();
        }
    }
}
