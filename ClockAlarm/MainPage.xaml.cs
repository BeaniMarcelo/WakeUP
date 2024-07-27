using System;
using System.Collections.ObjectModel;
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

        #if ANDROID
        private MediaPlayer _mediaPlayer;
        #endif

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
                IsEnabled = true
            };

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

        private async void TriggerAlarm()
        {
            #if ANDROID
            _mediaPlayer = MediaPlayer.Create(global::Android.App.Application.Context, RingtoneManager.GetDefaultUri(RingtoneType.Alarm));
            _mediaPlayer?.Start();
            #elif IOS
            // iOS implementation (using AVFoundation to play a sound)
            // This part needs to be implemented as per iOS requirements
            // Example:
            // var alarmSound = new NSUrl("DefaultAlarmSound.aiff");
            // var audioPlayer = AVAudioPlayer.FromUrl(alarmSound);
            // audioPlayer.Play();
            #endif

            // Show the alert and wait for user to dismiss it
            await DisplayAlert("Alarm", "Time to wake up!", "OK");

            // Stop the media player after the alert is dismissed
             #if ANDROID
            _mediaPlayer?.Stop();
            _mediaPlayer?.Release();
            _mediaPlayer = null;
            #endif
        }
    }
}
