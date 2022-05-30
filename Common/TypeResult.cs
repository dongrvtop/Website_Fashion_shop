using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public class TypeResult
    {
        public class ListResult
        {
            public object data { get; set; }
            public int pageCount { get; set; }
            public int pageStart { get; set; }
            public int pageEnd { get; set; }
            public int totalRowCount { get; set; }
        }
    }
}
