using System;

namespace project_notifications
{
    // Delat gränssnitt för Android och iOS
    public interface INotificationHandler
    {
        void SendNotification(string title, string message);
    }
}
