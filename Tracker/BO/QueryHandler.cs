using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.IO;
using System.Linq;
using System.Reflection;
using Tracker.Model;

namespace Tracker.BO
{
    public abstract class QueryHandler : IDisposable
    {
        private SQLiteConnection _connection;

        private string _connectionString;
        private QueryString _queryObj;
        public SQLiteConnection Connection
        {
            get { return _connection; }
        }

        public QueryString QueryObj
        {
            get { return _queryObj; }
        }

        protected QueryHandler()
        {
            _queryObj = new QueryString();
            _connectionString = "data source=" + _queryObj.DatabaseFileName;
        }

        protected void Initialize_Connection(ref string InitializeQuery)
        {
            if (!File.Exists(QueryObj.DatabaseFileName))
            {
                SQLiteConnection.CreateFile(QueryObj.DatabaseFileName);        // Create the file which will be hosting our database
            }
            _connection = new SQLiteConnection("data source="+ QueryObj.DatabaseFileName);
            ExecuteQuery(InitializeQuery);
        }

        protected object ExecuteQuery(string CommandString, SQLiteExecuteType QueryType = SQLiteExecuteType.NonQuery)
        {
            return SQLiteCommand.Execute(CommandString, QueryType, _connectionString, null);
        }

        public IList<T> GetData<T>(ref string QueryString)
        {
            IList<T> ResultData = Activator.CreateInstance<List<T>>();

            SQLiteDataReader reader = ExecuteQuery(QueryString, SQLiteExecuteType.Reader) as SQLiteDataReader;

            PropertyInfo[] modelProperties = typeof(T).GetProperties();
            while (reader.Read())
            {
                T returnedObject = Activator.CreateInstance<T>();

                foreach (var modelProperty in modelProperties)
                {
                    MappingAttribute[] attributes = modelProperty.GetCustomAttributes<MappingAttribute>(true).ToArray();
                    int index = reader.GetOrdinal(attributes?[0].ColumnName);
                    if (index != -1)
                    {
                        modelProperty.SetValue(returnedObject, Convert.ChangeType(reader[index], modelProperty.PropertyType), null);
                    }
                }

                ResultData.Add(returnedObject);
            }
            reader.Close();
            return ResultData;
        }

        public void Dispose()
        {
            _connection.Dispose();
        }
    }
}
