using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tracker.Model;

namespace Tracker.BO
{
    public class InvestmentQueryHandler : QueryHandler,
                                          IQueryHandler<Investment>
    {

        private static InvestmentQueryHandler _queryHandler = new InvestmentQueryHandler();

        public static IQueryHandler<Investment> GetDBConnector()
        {
            return _queryHandler;
        }

        public InvestmentQueryHandler()
        {
            Initialize_Connection(ref QueryObj.CreateInvestmentTableQuery);
        }

        public bool Add(Investment Data)
        {
            ExecuteQuery(string.Format(QueryObj.InsertInvestmentData, Data.Time.ToString("dd MMM yyyy"), Data.Name, Data.AMC, Data.Amount, Data.Units));
            return true;
        }

        public IList<Investment> GetData()
        {
            return GetInvestment(ref QueryObj.InvestmentDataQuery);
        }

        public IList<Investment> GetDataByCategory()
        {
            throw new NotImplementedException();
        }

       
        private IList<Investment> GetInvestment(ref string QueryString)
        {
            return _queryHandler.GetData<Investment>(ref QueryString);
        }

        public Task<bool> ImportDataBase(IList<Investment> Data)
        {
            throw new NotImplementedException();
        }
    }
}
