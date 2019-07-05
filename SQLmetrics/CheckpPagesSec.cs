using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//The checkpoint pages per second counter measures the number of pages written to disk by a 
//checkpoint operation. You should watch this counter over time to establish a baseline for
//your systems. Once a baseline value has been established you can watch this value to see 
//if it is climbing. If this counter is climbing, it might mean you are running into memory
//pressures that are causing dirty pages to be flushed to disk more frequently than normal.

namespace SQLmetrics
{
    class CheckpPagesSec
    {
        public static string query()
        {
            var qStr = "SELECT cntr_value FROM sys.dm_os_performance_counters WHERE counter_name = 'Checkpoint pages/sec' ;";
            return qStr;
        }

        public static List<object> value(long metric)
        {
            List<object> value = new List<object>();
            metric = metric / 1000;
            value.Add(Convert.ToInt32(metric)); //convert to 32 bit for PRTGs sake
            value.Add("ok");
            value.Add(0);
            return value;
            //PRTG will not accept anything over 32bit or 4.x billion, in my case
            //the production environment was giving me a value of 11,963,903,177
            //solution was to work with 1000 or k, turning the above to 11,963,903 K
            //unfortunately if this grows larger the code will need to be adjusted.
            //Alternatively I could add a second argument to the command line switch
            //denoting K,M etc. And depending in code which is used devide either by
            //1000 or 1000000 respectively.            
        }
    }
}
