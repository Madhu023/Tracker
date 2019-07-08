using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tracker.Model;

namespace Tracker.BO
{
    public interface IQueryHandler<T>
    {
        bool ImportDataBase(IList<T> ExpenseData);

        bool AddExpense(T ExpenseInfo);

        IList<T> GetExpenseData();

        IList<T> GetExpenseDataByCategory();
    }
}
