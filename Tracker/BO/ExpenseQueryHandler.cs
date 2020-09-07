using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Threading.Tasks;
using Tracker.Model;

namespace Tracker.BO
{
    public class ExpenseQueryHandler : QueryHandler, 
                                       IQueryHandler<Expense>
    {
        private static ExpenseQueryHandler _queryHandler = new ExpenseQueryHandler();

        public static IQueryHandler<Expense> GetDBConnector()
        {
            return _queryHandler;
        }

        public ExpenseQueryHandler()
        {
            Initialize_Connection(ref QueryObj.CreateExpenseTableQuery);
        }

        public IList<Expense> GetData()
        {
            return GetExpense(ref QueryObj.ExpenseDataQuery);
        }

        private IList<Expense> GetExpense(ref string QueryString)
        {
            return _queryHandler.GetData<Expense>(ref QueryString);
        }

        public IList<Expense> GetDataByCategory()
        {
            return GetExpense(ref QueryObj.ExpenseDataQueryByType);
        }

        public bool Add(Expense ExpenseInfo)
        {
            ExecuteQuery(string.Format(QueryObj.InsertExpenseDataQuery, ExpenseInfo.Time.ToString("dd MMM yyyy"), ExpenseInfo.Type, ExpenseInfo.Amount, ExpenseInfo.Description));
            return true;
        }

        async Task<bool> IQueryHandler<Expense>.ImportDataBase(IList<Expense> Data)
        {
            foreach (var ExpenseInfo in Data)
            {
                if (!Add(ExpenseInfo))
                    return false;
            }
            return true;
        }
    }
}
