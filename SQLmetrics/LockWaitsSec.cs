using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//# In order for SQL Server to manage concurrent users on the system, SQL Server needs to lock resources 
//# from time to time. The lock waits per second counter tracks the number of times per second that 
//# SQL Server is not able to retain a lock right away for a resource. Ideally you don't want any request 
//# to wait for a lock. Therefore you want to keep this counter at zero, or close to zero at all times

namespace SQLmetrics
{
    class LockWaitsSec
    {
        public static string query()
        {
            var qStr = "SELECT cntr_value FROM sys.dm_os_performance_counters WHERE counter_name = 'Lock Waits/sec' AND instance_name = '_Total';";
            return qStr;
        }

        public static List<object> value(int metric)
        {
            List<object> value = new List<object>();
            value.Add(metric);
            value.Add("ok");
            value.Add(0);
            return value;
        }
    }
}