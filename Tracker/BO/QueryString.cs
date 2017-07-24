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
                                          [Description] NVARCHAR(2048) NULL,
                                          [Amount] INTEGER NOT NULL,
                                          [Date] TEXT(2018) NULL
                                          )";

        public string InsertDataQuery = @"INSERT OR REPLACE INTO ExpenseTable (Description, Amount, Date) Values ('{0}','{1}','{2}')";

        public string ExpenseCountQuery = @"SELECT SUM(Amount) As Total FROM ExpenseTable";

        public string ExpenseDataQuery = @"SELECT * FROM ExpenseTable";

    }
}
