using System;
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
        private DateTime? alarmTime;

        public MainPage()
        {
            InitializeComponent();
        }

        private void OnSetAlarmClicked(object sender, EventArgs e)
        {
            alarmTime = DateTime.Today.Add(alarmTimePicker.Time);
            if (alarmTime <= DateTime.Now)
            {
                alarmTime = alarmTime.Value.AddDays(1);
            }

            DisplayAlert("Alarm Set", $"Alarm set for {alarmTime.Value.ToShortTimeString()}", "OK");
            Device.StartTimer(TimeSpan.FromSeconds(1), CheckAlarm);
        }

        private void OnCancelAlarmClicked(object sender, EventArgs e)
        {
            alarmTime = null;
            DisplayAlert("Alarm Canceled", "Alarm has been canceled", "OK");
        }

        private bool CheckAlarm()
        {
            if (alarmTime.HasValue && DateTime.Now >= alarmTime.Value)
            {
                alarmTime = null;
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
            _mediaPlayer.Start();
            #elif IOS
                        // iOS implementation (using AVFoundation to play a sound)
            // This part needs to be implemented as per iOS requirements
            // Example:
            // var alarmSound = new NSUrl("DefaultAlarmSound.aiff");
            // var audioPlayer = AVAudioPlayer.FromUrl(alarmSound);
            // audioPlayer.Play();
            #endif
        }
    }
}
