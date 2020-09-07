using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Threading.Tasks;
using Tracker.Model;

namespace Tracker.BO
{
    public class IncomeQueryHandler : QueryHandler,
                                      IQueryHandler<Income>
    {

        private static IncomeQueryHandler _queryHandler = new IncomeQueryHandler();

        private IList<Income> GetExpense(ref string QueryString)
        {
            return _queryHandler.GetData<Income>(ref QueryString);
        }

        public static IQueryHandler<Income> GetDBConnector()
        {
            return _queryHandler;
        }

        public IncomeQueryHandler()
        {
            Initialize_Connection(ref QueryObj.CreateIncomeTableQuery);
        }

        public bool Add(Income Data)
        {
            ExecuteQuery(string.Format(QueryObj.InsertIncomeDataQuery, Data.Time.ToString("dd MMM yyyy"), Data.Type, Data.Amount));
            return true;
        }

        public IList<Income> GetData()
        {
            return GetExpense(ref QueryObj.IncomeDataQuery);
        }

        public IList<Income> GetDataByCategory()
        {
            return GetExpense(ref QueryObj.IncomeDataQueryByType);
        }

        public Task<bool> ImportDataBase(IList<Income> Data)
        {
            throw new NotImplementedException();
        }
    }
}
