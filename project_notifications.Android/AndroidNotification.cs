using Android.App;
using AndroidApp = Android.App.Application;
using AndroidX.Core.App;
using Xamarin.Forms;

// DependencyAttribute is registering the Android implementation of the ICustomNotification
[assembly: Dependency(typeof(project_notifications.Droid.AndroidNotificationManager))]
namespace project_notifications.Droid
{
    // implementation of ICustomNotification
    public class AndroidNotificationManager : INotificationHandler
    {
        // Define the method to call the notification
        public void SendNotification(string title, string message)
        {
            // Channel properties
            var channelId = "default";
            var channelName = "notify user";
            var channelDescription = "local notification";
            var channel = new NotificationChannel(channelId, channelName, NotificationImportance.Default)
            {
                Description = channelDescription
            };
            // Create notification channel
            var notificationManager = (NotificationManager)AndroidApp.Context.GetSystemService(Android.Content.Context.NotificationService);
            notificationManager.CreateNotificationChannel(channel);

            // Instantiate the builder and set notification elements:
            NotificationCompat.Builder builder = new NotificationCompat.Builder(AndroidApp.Context, channelId)
                .SetContentTitle(title)
                .SetContentText(message)
                .SetSmallIcon(Resource.Drawable.notify_panel_notification_icon_bg)
                .SetDefaults((int)NotificationDefaults.Sound | (int)NotificationDefaults.Vibrate);

            // Build the notification:
            Notification notification = builder.Build();

            // Publish the notification:
            const int notificationId = 0;
            notificationManager.Notify(notificationId, notification);
        }
    }
}

