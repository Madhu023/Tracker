using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tracker.Model
{
    public class Investment
    {
        [Mapping(ColumnName = "ID")]
        public Int64 TransactionId { get; set; }

        [Mapping(ColumnName = "Date")]
        public DateTime Time { get; set; } = DateTime.Now;

        [Mapping(ColumnName = "Name")]
        public string Name { get; set; }

        [Mapping(ColumnName = "AMC")]
        public string AMC { get; set; }

        [Mapping(ColumnName = "Amount")]
        public double Amount { get; set; }

        [Mapping(ColumnName = "Units")]
        public double Units { get; set; }
    }
}
