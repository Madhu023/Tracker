using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tracker.BO
{
    public sealed class QueryString
    {
        public string DatabaseFileName = "Expense.db3";

        public string CreateTableQuery = @"CREATE TABLE IF NOT EXISTS ExpenseTable (
                                          [ID] INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT,
                                          [Date] TEXT(2018) NULL,
                                          [Type] TEXT(2018) NULL,
                                          [Amount] REAL NOT NULL
                                          )";

        public string InsertDataQuery = @"INSERT OR REPLACE INTO ExpenseTable (Date, Type, Amount) Values ('{0}','{1}','{2}')";

        public string ExpenseCountQuery = @"SELECT SUM(Amount) As Total FROM ExpenseTable";

        public string ExpenseDataQueryByType = @"SELECT Date, Type, SUM(Amount) FROM ExpenseTable GROUP BY Type";

        public string ExpenseDataQuery = @"SELECT Date, Type, Amount FROM ExpenseTable";

        public string ExpenseTypesQuery = @"SELECT Type FROM ExpenseTable GROUP BY Type";

        public string TimedExpenseDataQuery = @"SELECT * FROM ExpenseTable";

    }
}
