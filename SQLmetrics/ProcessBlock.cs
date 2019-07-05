using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//The processes blocked counter identifies the number of blocked processes. When one process 
//is blocking another process, the blocked process cannot move forward with its execution plan 
//until the resource that is causing it to wait is freed up. Ideally you don't want to see any 
//blocked processes. When processes are being blocked you should investigate.

namespace SQLmetrics
{
    class ProcessBlock
    {
        public static string query()
        {
            var qStr = "SELECT cntr_value FROM sys.dm_os_performance_counters WHERE counter_name = 'Processes Blocked' ;";
            return qStr;
        }

        public static List<object> value(int metric)
        {
            List<object> value = new List<object>();
            if (metric > 0)
            {
                value.Add(metric);
                value.Add("WARNING!");
                value.Add(1);
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
