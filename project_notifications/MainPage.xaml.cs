using System;
using System.ComponentModel;
using Xamarin.Forms;

namespace project_notifications
{
    public partial class MainPage : ContentPage
    {
        INotificationHandler notification;

        DateTime _triggerTime;

        public MainPage()
        {
            InitializeComponent();
            // Initiera referensen till gränssnittet för att kunna utföra meddelandet
            notification = DependencyService.Get<INotificationHandler>();
            // Ansvarar för att påbörja utförandet av meddelandet enligt schemaläggningen
            Device.StartTimer(TimeSpan.FromSeconds(1), OnTimerTick);
        }

        bool OnTimerTick()
        {
            if (_switch.IsToggled)
            {
                if (DateTime.Now >= _triggerTime)
                {
                    _switch.IsToggled = false; // Inaktiverad för att skapa ytterligare ett meddelande
                    notification.SendNotification("Min app", "Glöm inte bort att röra på dig!");
                }
            }
            return true;
        }

        // Metod som ansvarar för att fånga tiden för TimePicker-elementet x:Name="firstTime"
        void OnTimePickerPropertyChanged(object sender, PropertyChangedEventArgs args)
        {
            if (args.PropertyName == "Time")
            {
                SetTriggerTime();
            }
        }

        // Metod som BESTÄMMER eller STÄLLER in körningstiden baserat på OnTimePickerPropertyChanged PROPERTY för TimePicker-elementet x:Name="timePicker"
        void SetTriggerTime()
        {
            if (_switch.IsToggled)
            {
                _triggerTime = DateTime.Today + firstTime.Time;

                if (_triggerTime < DateTime.Now)
                {
                    _triggerTime += TimeSpan.FromDays(1);
                }
            }
        }

        // Fångar värdet på Toggled-egenskapen för Switch-elementet x:Name="_switchActive"
        void OnSwitchToggled(object sender, ToggledEventArgs args)
        {
            SetTriggerTime();
        }
    }
}
