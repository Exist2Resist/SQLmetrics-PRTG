using System.Collections.Generic;

// Batch Requests/Sec measures the number of batches SQL Server is receiving per second. This counter 
// is a good indicator of how much activity is being processed by your SQL Server box. The higher the 
// number, the more queries are being executed on your box. Like many counters, there is no single number
// that can be used universally to indicate your machine is too busy. Today’s machines are getting more 
// and more powerful all the time and therefore can process more batch requests per second. You should 
// review this counter over time to determine a baseline number for your environment.

namespace SQLmetrics
{
    class BatchReqSec
    {
        public static string query()
        {
            var qStr = "SELECT cntr_value FROM sys.dm_os_performance_counters WHERE counter_name = 'Batch Requests/sec' ;";
            return qStr;
        }

        public static List<object> value(int metric) //needs to be int64
        {
            List<object> value = new List<object>();
            if (metric <= 10000)
            {
                value.Add(metric);
                value.Add("WARNING!");
                value.Add(1); //exit code
            }
            else
            {
                value.Add(metric);
                value.Add("ok");
                value.Add(0);
            }
            return value;
        }
    }
}
