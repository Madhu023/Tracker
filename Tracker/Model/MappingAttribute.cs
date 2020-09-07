using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tracker.Model
{
    [AttributeUsage(AttributeTargets.Property, Inherited = true)]
    [Serializable]
    public class MappingAttribute : Attribute
    {
        public string ColumnName;
    }
}
