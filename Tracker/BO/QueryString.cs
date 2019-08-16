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

        public string CreateExpenseTableQuery = @"CREATE TABLE IF NOT EXISTS ExpenseTable (
                                                  [ID] INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT,
                                                  [Date] TEXT(2018) NULL,
                                                  [Type] TEXT(2018) NULL,
                                                  [Amount] REAL NOT NULL,
                                                  [Description] TEXT(2018) NULL
                                                  )";

        public string CreateIncomeTableQuery = @"CREATE TABLE IF NOT EXISTS IncomeTable(
                                                [ID] INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT, 
                                                [Date] TEXT(2018) NULL,
                                                [Type] TEXT(2018) NULL,
                                                [Amount] REAL NOT NULL
                                                )";
        public string CreateInvestmentTableQuery = @"CREATE TABLE IF NOT EXISTS InvestmentTable(
                                                    [ID] INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT, 
                                                    [Date] TEXT(2018) NULL,
                                                    [FundName] TEXT(2018) NULL,
                                                    [Amount] REAL NOT NULL
                                                    )";

        public string InsertExpenseDataQuery = @"INSERT OR REPLACE INTO ExpenseTable (Date, Type, Amount, Description) Values ('{0}','{1}','{2}','{3}')";

        public string ExpenseCountQuery = @"SELECT SUM(Amount) As Total FROM ExpenseTable";

        public string ExpenseDataQueryByType = @"SELECT Date, Type, SUM(Amount) FROM ExpenseTable GROUP BY Type";

        public string ExpenseDataQuery = @"SELECT Date, Type, Amount FROM ExpenseTable";

        public string ExpenseTypesQuery = @"SELECT Type FROM ExpenseTable GROUP BY Type";

        public string TimedExpenseDataQuery = @"SELECT * FROM ExpenseTable";

        public string InsertIncomeDataQuery = @"INSERT OR REPLACE INTO IncomeTable (Date, Type, Amount) Values ('{0}','{1}','{2}')";

        public string IncomeDataQuery = @"SELECT Date, Type, Amount FROM IncomeTable";

        public string IncomeDataQueryByType = @"SELECT Date, Type, SUM(Amount) FROM IncomeTable GROUP BY Type";

    }
}
