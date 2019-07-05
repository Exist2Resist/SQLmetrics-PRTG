using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// The SQL Compilations/Sec measure the number of times SQL Server compiles an execution plan per 
// second. Compiling an execution plan is a resource-intensive operation. Compilations/Sec should 
// be compared with the number of Batch Requests/Sec to get an indication of whether or not 
// complications might be hurting your performance. To do that, divide the number of batch requests 
// by the number of compiles per second to give you a ratio of the number of batches executed per 
// compile. Ideally you want to have one compile per every 10 batch requests.

//$brsc = New-Object System.Diagnostics.PerformanceCounter
//$brsc.CategoryName = 'SQLServer:SQL Statistics'
//$brsc.CounterName = 'Batch Requests/sec' valid
//$brs = $brsc.NextValue()
//$brs_benchmark = 10000 #anything below 10000 means trouble in Springfield Power Plant

//$scsc = New-Object System.Diagnostics.PerformanceCounter
//$scsc.CategoryName = 'SQLServer:SQL Statistics'
//$scsc.CounterName = 'SQL Compilations/Sec' valid
//$scs = $scsc.NextValue()
//$scs_benchmark = ($brs/10)

namespace SQLmetrics
{
    class SQLCompSec
    {
        public static string query()
        {
            var qStr = "SELECT ( cast(BRS.cntr_value as float) / cast(SCS.cntr_value as float) )"
                + " FROM(SELECT * FROM sys.dm_os_performance_counters WHERE counter_name = 'SQL Compilations/sec') SCS"
                + " CROSS JOIN"
                + " (SELECT * FROM sys.dm_os_performance_counters WHERE counter_name = 'Batch Requests/sec') BRS;";
            return qStr;
        }

        public static List<object> value(float metric)
        {
            List<object> value = new List<object>();
            if (metric < 10)
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
