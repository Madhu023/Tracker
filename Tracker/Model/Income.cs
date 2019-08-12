using System;

namespace Tracker.Model
{
    public class Income
    {
        public DateTime Time { get; set; } = DateTime.Now;

        public String Type { get; set; }

        public Double Amount { get; set; }
    }
}
