using System;
using System.Collections.Generic;
using System.Data.SQLite;
using Tracker.Model;

namespace Tracker.BO
{
    public class IncomeQueryHandler : QueryHandler,
                                      IQueryHandler<Income>
    {

        private static IQueryHandler<Income> _queryHandler;

        private IList<Income> GetExpense(ref string QueryString)
        {
            IList<Income> Data = new List<Income>();
            SQLiteCommand command = GetSqlCommand(QueryString);

            Connection.Open();
            using (SQLiteDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    Data.Add(new Income
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

        public static IQueryHandler<Income> GetDBConnector()
        {
            if (null == _queryHandler)
            {
                _queryHandler = new IncomeQueryHandler();
            }
            return _queryHandler;
        }

        public IncomeQueryHandler()
        {
            Initialize_Connection(ref QueryObj.CreateIncomeTableQuery);
        }

        public bool Add(Income Data)
        {
            return ExecuteQuery(string.Format(QueryObj.InsertIncomeDataQuery, Data.Time.ToString("dd MMM yyyy"), Data.Type, Data.Amount));
        }

        public IList<Income> GetData()
        {
            return GetExpense(ref QueryObj.IncomeDataQuery);
        }

        public IList<Income> GetDataByCategory()
        {
            return GetExpense(ref QueryObj.IncomeDataQueryByType);
        }

        public bool ImportDataBase(IList<Income> Data)
        {
            throw new NotImplementedException();
        }
    }
}
