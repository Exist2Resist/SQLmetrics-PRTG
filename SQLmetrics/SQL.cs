using System;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Linq;

namespace SQLmetrics
{
    class SQL
    {

        public static int SQLsort(string argument)
        {
            int ret=0; //return\exit code
            ret = SQLquery(argument);
            return ret;
        }
        private static int SQLquery(string argument)
        {
            int ret = 4;
            var connectionString = cStr();
            var queryString = qStr(argument);

            SqlConnection connection = new SqlConnection(connectionString);
            try
            {
                connection.Open();

                SqlCommand command = new SqlCommand(queryString, connection);
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())  //PRTG is expecting a return in the format of int:string \n exit_code
                {
                    var rdVal = Convert.ToInt32(reader[0]);
                    var rdValdec = Convert.ToSingle(reader[0]); //need decimal precision
                    var rdVal64 = Convert.ToInt64(reader[0]); //need 64 bit intiger, PRTG can not handle 64 bit intigers so we need to return 32 bit even when working with 64 bit.
                    List<object> value = new List<object>();
                    switch (argument)
                    {
                        case "pagelfx":
                            value = PageLifeEx.value(rdVal);                           
                            break;
                        case "batreqsec":
                            value = BatchReqSec.value(rdVal);
                            break;
                        case "sqlcomsec":                           
                            value = SQLCompSec.value(rdValdec);
                            //result is # of baches executed per compile, ideally +10:1 ratio
                            break;
                        case "sqlrecomsec":                            
                            value = SQLRecompSec.value(rdValdec);
                            break;
                        case "userconn":
                            value = UserConn.value(rdVal);
                            break;
                        case "lckwtssec":
                            value = LockWaitsSec.value(rdVal);
                            break;
                        case "pgsplitsec":
                            value = PageSplitsSec.value(rdValdec);
                            break;
                        case "procblock":
                            value = ProcessBlock.value(rdVal);
                            break;
                        case "chkpgsec":
                            value = CheckpPagesSec.value(rdVal64);
                            //this result comes back in 1K, 39 = 39K or 39 000
                            break;
                        default:
                            break;
                    }
                    Console.WriteLine(Convert.ToInt32(value.ElementAt(0)) + ":" + value.ElementAt(1));
                    ret = Convert.ToInt32(value.ElementAt(2));                    
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(Convert.ToString(e));
                ret = 3;
            }
            finally
            {
                connection.Close();
            }
            return ret;
        }

        private static string cStr()
        {
            var SQLsrvr = //"user id= ;" +
                       //"password= ;" + 
            "server=PJLM-S01;" +   //add server name
            "Trusted_Connection=yes;" +
            "database=PAFM;" +  //add database name
            "connection timeout=30";
            return SQLsrvr;
        }

        private static string qStr(string argument)
        {
            string str = "";
            switch (argument)
            {
                case "pagelfx":
                    str = PageLifeEx.query();                    
                    break;
                case "batreqsec":
                    str = BatchReqSec.query();
                    break;
                case "sqlcomsec":
                    str = SQLCompSec.query();
                    break;
                case "sqlrecomsec":
                    str = SQLRecompSec.query();
                    break;
                case "userconn":
                    str = UserConn.query();
                    break;
                case "lckwtssec":
                    str = LockWaitsSec.query();
                    break;
                case "pgsplitsec":
                    str = PageSplitsSec.query();
                    break;
                case "procblock":
                    str = ProcessBlock.query();
                    break;
                case "chkpgsec":
                    str = CheckpPagesSec.query();
                    break;
                default:
                    break;
            }
            return str;
        }
    }
}