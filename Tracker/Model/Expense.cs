using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tracker.Model
{
    public class Expense
    {
        public String Description { get; set; }
        public DateTime Time { get; set; }

        public Double Amount { get; set; }

        [DefaultValue("Groceries")]
        public String Type { get; set; } = "Groceries";
    }
}
