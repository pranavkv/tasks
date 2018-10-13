using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FeedbackAnalyzer
{
    class ApiException: Exception
    {
        public ApiException()
        {
            
        }

        public ApiException(String msg) : base(msg)
        {

        }
    }
}
