using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.DataAccess.Domains
{
    public class ResponseListResult<T>
    {
        public bool Status { get; set; }
        public T Data { get; set; }
    }

}
