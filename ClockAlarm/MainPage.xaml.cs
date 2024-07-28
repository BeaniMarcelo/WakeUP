using System;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using ClockAlarm.Models;
using Microsoft.Maui.Controls;
#if ANDROID
using Android.Media;
using Android.App;
using Android.Content;
using Android.Net;
#endif

namespace ClockAlarm
{
    public partial class MainPage : ContentPage
    {
        public ObservableCollection<Alarm> Alarms { get; set; }

        public MainPage()
        {
            InitializeComponent();
            Alarms = new ObservableCollection<Alarm>();
            BindingContext = this;
        }

        [Obsolete]
        private void OnAddAlarmClicked(object sender, EventArgs e)
        {
            var newAlarm = new Alarm
            {
                Time = DateTime.Today.Add(alarmTimePicker.Time),
                IsEnabled = true,
                DaysOfWeek = new List<string>() // Add selected days of the week here
            };

            // Add the selected days to the alarm
            if (mondayButton.BackgroundColor == Colors.Green) newAlarm.DaysOfWeek.Add("Monday");
            if (tuesdayButton.BackgroundColor == Colors.Green) newAlarm.DaysOfWeek.Add("Tuesday");
            if (wednesdayButton.BackgroundColor == Colors.Green) newAlarm.DaysOfWeek.Add("Wednesday");
            if (thursdayButton.BackgroundColor == Colors.Green) newAlarm.DaysOfWeek.Add("Thursday");
            if (fridayButton.BackgroundColor == Colors.Green) newAlarm.DaysOfWeek.Add("Friday");
            if (saturdayButton.BackgroundColor == Colors.Green) newAlarm.DaysOfWeek.Add("Saturday");
            if (sundayButton.BackgroundColor == Colors.Green) newAlarm.DaysOfWeek.Add("Sunday");

            if (newAlarm.Time <= DateTime.Now)
            {
                newAlarm.Time = newAlarm.Time.AddDays(1);
            }

            Alarms.Add(newAlarm);
            Device.StartTimer(TimeSpan.FromSeconds(1), () => CheckAlarm(newAlarm));
        }

        private void OnDeleteAlarmClicked(object sender, EventArgs e)
        {
            var button = sender as Button;
            var alarm = button?.CommandParameter as Alarm;
            if (alarm != null)
            {
                Alarms.Remove(alarm);
            }
        }

        private bool CheckAlarm(Alarm alarm)
        {
            if (alarm.IsEnabled && DateTime.Now >= alarm.Time)
            {
                alarm.IsEnabled = false; // Disable the alarm after it triggers
                TriggerAlarm();
                return false; // Stop the timer
            }
            return true; // Continue checking
        }

        private void TriggerAlarm()
        {
            // Trigger a notification or sound
            DisplayAlert("Alarm", "Time to wake up!", "OK");

#if ANDROID
            var _mediaPlayer = MediaPlayer.Create(global::Android.App.Application.Context, RingtoneManager.GetDefaultUri(RingtoneType.Alarm));
            _mediaPlayer?.Start();
#elif IOS
            // iOS implementation (using AVFoundation to play a sound)
            // This part needs to be implemented as per iOS requirements
            // Example:
            // var alarmSound = new NSUrl("DefaultAlarmSound.aiff");
            // var audioPlayer = AVAudioPlayer.FromUrl(alarmSound);
            // audioPlayer.Play();
#endif
        }

        private void OnDayButtonClicked(object sender, EventArgs e)
        {
            var button = sender as Button;
            if (button != null)
            {
                if (button.BackgroundColor == Colors.Gray)
                {
                    button.BackgroundColor = Colors.Green;
                    button.TextColor = Colors.Black;
                }
                else
                {
                    button.BackgroundColor = Colors.Gray;
                    button.TextColor = Colors.White;
                }
            }
        }
    }
}
