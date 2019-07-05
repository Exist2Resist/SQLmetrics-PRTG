using System;

namespace SQLmetrics
{
    class Program
    {
        //Program.cs -> SQL.cs -> To supplied argument method
        static void Main(string[] args)
        {
            //Console.WriteLine(args[0]);
            int ret;
            if (args.Length > 1)
            {
                Console.WriteLine("Too many parameter switches,");
                Environment.Exit(2);
            }
            else
            {
                ret = SQL.SQLsort(args[0]);  //PRTG requires an exit code
                Environment.Exit(ret);                
            }          
        }
    }
}
//Exit codes for PRTG
//Success = 0,
//WARNING = 1,
//System_Error = 2,
//Protocol_Error = 3,
//Content_Error = 4