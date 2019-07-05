using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//This counter measures the number of times SQL Server had to split a page when updating or 
//inserting data per second. Page splits are expensive, and cause your table to perform more poorly
//due to fragmentation. Therefore, the fewer page splits you have the better your system will perform. 
//Ideally this counter should be less than 20% of the batch requests per second.

namespace SQLmetrics
{
    class PageSplitsSec
    {
        public static string query()
        {
            var qStr = "SELECT (cast(PSS.cntr_value as float) / cast(BRS.cntr_value as float))"
                + " FROM(SELECT * FROM sys.dm_os_performance_counters WHERE counter_name = 'Page Splits/sec') PSS"
                + " CROSS JOIN"
                + " (SELECT * FROM sys.dm_os_performance_counters WHERE counter_name = 'Batch Requests/sec') BRS;";
            return qStr;
        }
        public static List<object> value(float metric)
        {
            List<object> value = new List<object>();
            if (metric > 0.19 )
            {
                value.Add(metric * 100);
                value.Add("WARNING!");
                value.Add(1);  //exit code
            }
            else
            {
                value.Add(metric * 100);
                value.Add("ok");
                value.Add(0);
            }
            return value;
        }
    }
}
