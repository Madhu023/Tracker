using System;
using System.Collections.Generic;
using System.Data.SQLite;
using Tracker.Model;

namespace Tracker.BO
{
    public class ExpenseQueryHandler : QueryHandler, 
                                       IQueryHandler<Expense>
    {
        private static ExpenseQueryHandler _queryHandler;

        public static IQueryHandler<Expense> GetDBConnector()
        {
            if (null == _queryHandler)
            {
                _queryHandler = new ExpenseQueryHandler();
            }
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
            IList<Expense> Data = new List<Expense>();
            SQLiteCommand command = GetSqlCommand(QueryString);

            Connection.Open();
            using (SQLiteDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    Data.Add(new Expense
                    {
                        Time = Convert.ToDateTime(reader[0]),
                        Type = reader[1].ToString(),
                        Amount = Convert.ToDouble(reader[2].ToString()),
                        Description = reader[1].ToString()
                    });
                }
            }
            Connection.Close();
            command.Dispose();

            return Data;
        }

        public IList<Expense> GetDataByCategory()
        {
            return GetExpense(ref QueryObj.ExpenseDataQueryByType);
        }

        public bool Add(Expense ExpenseInfo)
        {
            return ExecuteQuery(string.Format(QueryObj.InsertExpenseDataQuery, ExpenseInfo.Time.ToString("dd MMM yyyy"), ExpenseInfo.Type, ExpenseInfo.Amount, ExpenseInfo.Description));
        }

        public bool ImportDataBase(IList<Expense> ExpenseData)
        {
            bool Result = true;
            foreach (var ExpenseInfo in ExpenseData)
            {
                Result = Add(ExpenseInfo);
                if (!Result)
                    break;
            }
            return Result;
        }
    }
}
