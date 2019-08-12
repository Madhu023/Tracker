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
        public Int64 TransactionId { get; set; }
        public DateTime Time { get; set; } = DateTime.Today;

        public Double Amount { get; set; } = 0.0;

        [DefaultValue("Groceries")]

        public String Type { get; set; } = "Groceries";

        public String Description { get; set; }
    }
}
