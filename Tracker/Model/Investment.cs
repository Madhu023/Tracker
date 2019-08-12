using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tracker.Model
{
    class Investment
    {
        public DateTime Time { get; set; } = DateTime.Now;

        public string Name { get; set; }

        public double Amount { get; set; }

        public double Units { get; set; }
    }
}
