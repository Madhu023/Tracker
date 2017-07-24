using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tracker.Model
{
    public class MonthlyExpenseData
    {
        public string Month { get; set; }
        public double Amount { get; set; }
    }


    public class YearlyExpenseData
    {
        public int Year { get; set; }

        public IList<MonthlyExpenseData> MonthlyData { get; set; }

        public double TotalExpense { get; set; }

        public YearlyExpenseData()
        {
            MonthlyData = new List<MonthlyExpenseData>();
        }
    }
}
