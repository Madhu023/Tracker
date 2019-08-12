using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tracker.Model;

namespace Tracker.BO
{
    public interface IQueryHandler<T>
    {
        bool ImportDataBase(IList<T> Data);

        bool Add(T Data);

        IList<T> GetData();

        IList<T> GetDataByCategory();
    }
}
