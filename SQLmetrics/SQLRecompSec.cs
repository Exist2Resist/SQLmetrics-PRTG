using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//When the execution plan is invalidated due to some significant event, SQL Server will re-compile it. 
//The Re-compilations/Sec counter measures the number of time a re-compile event was triggered per 
//second. Re-compiles, like compiles, are expensive operations so you want to minimize the number of 
//re-compiles. Ideally you want to keep this counter less than 10% of the number of Compilations/Sec.

namespace SQLmetrics
{
    class SQLRecompSec
    {
        public static string query()
        {
            var qStr = "SELECT ( cast(SRS.cntr_value as float) / cast(SCS.cntr_value as float) )"
                + " FROM(SELECT * FROM sys.dm_os_performance_counters WHERE counter_name = 'SQL Compilations/sec') SCS"
                + " CROSS JOIN"
                + " (SELECT * FROM sys.dm_os_performance_counters WHERE counter_name = 'SQL Re-Compilations/sec') SRS ;";
            return qStr;
        }

        public static List<object> value(float metric)
        {
            List<object> value = new List<object>();
            if (metric > .10)
            {
                value.Add(metric * 100);
                value.Add("WARNING!");
                value.Add(1);
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
