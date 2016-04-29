using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace NCas.Common
{
    public class ConfigSettings
    {
        public static TimeSpan ReservationAutoExpiration = TimeSpan.FromMinutes(15);
        /// <summary>主库ENode连接字符串
        /// </summary>
        public static string ENodeConnectionString { get; set; }
        /// <summary>主库连接字符串
        /// </summary>
        public static string CasConnectionString { get; set; }

        /// <summary>账号表
        /// </summary>
        public static string AccountTable { get; set; }
        /// <summary>账号Code索引表
        /// </summary>
        public static string AccountCodeIndexTable { get; set; }
        /// <summary>账号名称索引表
        /// </summary>
        public static string AccountNameIndexTable { get; set; }

        /// <summary>网站节点表
        /// </summary>
        public static string WebAppTable { get; set; }


        public static IPAddress BrokerIp { get; set; }

        public static int ProducerPort { get; set; }

        public static int ConsumerPort { get; set; }

        public static int AdminPort { get; set; }
        public static int CommandBindingPort { get; set; }

        public static void Initialize()
        {
            if (ConfigurationManager.ConnectionStrings["ENode"] != null)
            {
                ENodeConnectionString = ConfigurationManager.ConnectionStrings["ENode"].ConnectionString;
            }

            if (ConfigurationManager.ConnectionStrings["NCas"] != null)
            {
                CasConnectionString = ConfigurationManager.ConnectionStrings["NCas"].ConnectionString;
            }

            AccountTable = "Account";
            AccountCodeIndexTable = "AccountCodeIndex";
            AccountNameIndexTable = "AccountNameIndex";
            WebAppTable = "WebApp";


            if (ConfigurationManager.AppSettings["BrokerIp"] != null)
            {
                BrokerIp = IPAddress.Parse(ConfigurationManager.AppSettings["BrokerIp"]);
            }
            if (ConfigurationManager.AppSettings["ProducerPort"] != null)
            {
                ProducerPort = int.Parse(ConfigurationManager.AppSettings["ProducerPort"]);
            }
            if (ConfigurationManager.AppSettings["ConsumerPort"] != null)
            {
                ConsumerPort = int.Parse(ConfigurationManager.AppSettings["ConsumerPort"]);
            }
            if (ConfigurationManager.AppSettings["AdminPort"] != null)
            {
                AdminPort = int.Parse(ConfigurationManager.AppSettings["AdminPort"]);
            }

            if (ConfigurationManager.AppSettings["CommandBingingPort"] != null)
            {
                CommandBindingPort = int.Parse(ConfigurationManager.AppSettings["CommandBingingPort"]);
            }
        }
    }
}
