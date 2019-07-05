using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//The user connections counter identifies the number of different users that are connected to SQL Server 
//at the time the sample was taken. You need to watch this counter over time to understand your baseline 
//user connection numbers. Once you have some idea of your high and low water marks during normal usage 
//of your system, you can then look for times when this counter exceeds the high and low marks. If the 
//value of this counter goes down and the load on the system is the same, then you might have a bottleneck 
//that is not allowing your server to handle the normal load. Keep in mind though that this counter value 
//might go down just because less people are using your SQL Server instance.

namespace SQLmetrics
{
    class UserConn
    {
        public static string query()
        {
            var qStr = "select cntr_value from sys.dm_os_performance_counters where counter_name = 'User Connections';";
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
