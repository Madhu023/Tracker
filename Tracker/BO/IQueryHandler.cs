using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tracker.Model;

namespace Tracker.BO
{
    public interface IQueryHandler
    {
        bool ImportDataBase(IList<Expense> ExpenseData);

        IList<Expense> GetExpenseData();

        void TestFunction();
    }
}
