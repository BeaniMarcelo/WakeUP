using System.Collections.Generic;

namespace ClockAlarm.Models
{
    public class Alarm
    {
        public DateTime Time { get; set; }
        public bool IsEnabled { get; set; }
        public List<string> DaysOfWeek { get; set; } // New property to store the selected days of the week

        public Alarm()
        {
            DaysOfWeek = new List<string>();
        }
    }
}
