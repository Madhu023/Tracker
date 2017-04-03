using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.IO;
using Tracker.Model;

namespace Tracker.BO
{
    public class QueryHandler : IQueryHandler
    {
        private static QueryHandler _queryHandler;

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

        public static QueryHandler GetDBConnector()
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
            foreach (var data in ExpenseData)
            {
                ExecuteQuery(string.Format(QueryObj.InsertDataQuery, data.Description, data.Amount, data.Time.ToString("yyyy - MM - dd HH: mm:ss")));
            }
            return true;
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
            List<Expense> Data = new List<Expense>();
            SQLiteCommand command = GetSqlCommand(QueryObj.GetExpenseDataQuery);

            Connection.Open();
            using (SQLiteDataReader reader = command.ExecuteReader())
            {
                while(reader.Read())
                {
                    Data.Add(new Expense
                    {
                        Time = Convert.ToDateTime(reader["Date"]),
                        Description = reader["Description"].ToString(),
                        Amount = Convert.ToDouble(reader["Amount"].ToString())
                    });
                }
            }
            Connection.Close();
            command.Dispose();

            return Data;
        }
    }
}
