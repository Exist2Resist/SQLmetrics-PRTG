using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//The page life expectancy counter measures how long pages stay in the buffer cache in seconds. 
//The longer a page stays in memory, the more likely SQL Server will not need to read from disk 
//to resolve a query. You should watch this counter over time to determine a baseline for what 
//is normal in your database environment. Some say anything below 300 (or 5 minutes) means you 
//might need additional memory.

namespace SQLmetrics
{
    class PageLifeEx //Page Life Expectancy
    {
        public static string query()
        {
            var qStr = " SELECT cntr_value FROM " + 
                "sys.dm_os_performance_counters WHERE object_name = " +
                "'SQLServer:Buffer Manager' AND counter_name = 'Page life expectancy';";
            return qStr;
        }

        public static List<object> value(int metric)
        {           
            List<object> value = new List<object>();
            if (metric <= 300)
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
