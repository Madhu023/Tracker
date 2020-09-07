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
        [Mapping(ColumnName = "ID")]
        public UInt64 TransactionId { get; set; }

        [Mapping(ColumnName = "Date")]
        public DateTime Time { get; set; } = DateTime.Today;

        [Mapping(ColumnName ="Amount")]
        public Double Amount { get; set; } = 0.0;

        [DefaultValue("Groceries")]
        [Mapping(ColumnName ="Type")]
        public String Type { get; set; } = "Groceries";

        [Mapping(ColumnName = "Description")]
        public String Description { get; set; }
    }
}
