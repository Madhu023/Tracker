using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.IO;
using System.Linq;
using Tracker.Model;

namespace Tracker.BO
{
    public abstract class QueryHandler
    {
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

        protected QueryHandler()
        {
            QueryObj = new QueryString();
        }

        protected void Initialize_Connection(ref string InitializeQuery)
        {
            if (!File.Exists(QueryObj.DatabaseFileName))
            {
                SQLiteConnection.CreateFile(QueryObj.DatabaseFileName);        // Create the file which will be hosting our database
            }
            Connection = new SQLiteConnection("data source="+ QueryObj.DatabaseFileName);
            ExecuteQuery(InitializeQuery);
        }

        protected bool ExecuteQuery(string CommandString)
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

        protected SQLiteCommand GetSqlCommand(string CommandString)
        {
            SQLiteCommand command = new SQLiteCommand(Connection);
            command.CommandText = CommandString;
            return command;
        }

        //private void GetExpense(ref string QueryString, ref IList<T> Data)
        //{
        //    //IList<Expense> Data = new List<Expense>();
        //    SQLiteCommand command = GetSqlCommand(QueryString);

        //    Connection.Open();
        //    using (SQLiteDataReader reader = command.ExecuteReader())
        //    {
        //        while (reader.Read())
        //        {
        //            Data.Add(Activator.CreateInstance(typeof(T), new object[]
        //            {
        //                Time = Convert.ToDateTime(reader[0]),
        //                Type = reader[1].ToString(),
        //                Amount = Convert.ToDouble(reader[2].ToString())
        //            }));
        //        }
        //    }
        //    Connection.Close();
        //    command.Dispose();

        //    //return Data;
        //}

    }
}
