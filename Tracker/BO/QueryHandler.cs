using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.IO;
using System.Linq;
using Tracker.Model;

namespace Tracker.BO
{
    public class QueryHandler : IQueryHandler<Expense>
    {
        private static IQueryHandler<Expense> _queryHandler;

        private SQLiteConnection _connection;

        private QueryString _queryObj;
        public SQLiteConnection Connection
        {
            get { return _connection; }
            set { _connection = value; }
        }

        public QueryString QueryObj
        {
            get { return _queryObj; }
            set { _queryObj = value; }
        }

        public static IQueryHandler<Expense> GetDBConnector()
        {
            if (null == _queryHandler)
            {
                _queryHandler = new QueryHandler();
            }
            return _queryHandler;
        }

        private QueryHandler()
        {
            QueryObj = new QueryString();
            Initialize_Connection();
        }

        private void Initialize_Connection()
        {
            if (!File.Exists(QueryObj.DatabaseFileName))
            {
                SQLiteConnection.CreateFile(QueryObj.DatabaseFileName);        // Create the file which will be hosting our database
            }
            Connection = new SQLiteConnection("data source="+ QueryObj.DatabaseFileName);
            ExecuteQuery(QueryObj.CreateTableQuery);
        }

        public bool ImportDataBase(IList<Expense> ExpenseData)
        {
            bool Result = true;
            foreach (var ExpenseInfo in ExpenseData)
            {
                Result = AddExpense(ExpenseInfo);
                if (!Result)
                    break;
            }
            return Result;
        }


        private bool ExecuteQuery(string CommandString)
        {
            Connection.Open();
            using (SQLiteCommand command = new SQLiteCommand(Connection))
            {
                command.CommandText = CommandString;
                command.ExecuteNonQuery();
            }
            Connection.Close();
            return true;
        }


        private SQLiteCommand GetSqlCommand(string CommandString)
        {
            SQLiteCommand command = new SQLiteCommand(Connection);
            command.CommandText = CommandString;
            return command;
        }

        public IList<Expense> GetExpenseData()
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
                        Amount = Convert.ToDouble(reader[2].ToString())
                    });
                }
            }
            Connection.Close();
            command.Dispose();

            return Data;
        }

        public IList<Expense> GetExpenseDataByCategory()
        {
            return GetExpense(ref QueryObj.ExpenseDataQueryByType);
        }

        public bool AddExpense(Expense ExpenseInfo)
        {
            return ExecuteQuery(string.Format(QueryObj.InsertDataQuery, ExpenseInfo.Time.ToString("dd MMM yyyy"), ExpenseInfo.Type, ExpenseInfo.Amount));
        }
    }
}
