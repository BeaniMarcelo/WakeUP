using System;
using System.Collections.Generic;

namespace ClockAlarm.Models
{
    public class Alarm
    {
        public DateTime Time { get; set; }
        public bool IsEnabled { get; set; }
        public List<string> DaysOfWeek { get; set; }

        public string DaysOfWeekDisplay
        {
            get
            {
                return DaysOfWeek != null && DaysOfWeek.Count > 0 ? string.Join(", ", DaysOfWeek) : "None";
            }
        }
    }
}
