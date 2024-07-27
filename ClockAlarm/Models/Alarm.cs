using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClockAlarm.Models
{
    public class Alarm
    {
        public DateTime Time { get; set; }
        public bool IsEnabled { get; set; }
    }
}
