using System;
using System.Configuration;
using System.Net;

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
        public static int BrokerProducerPort { get; set; }
        public static int BrokerConsumerPort { get; set; }
        public static int BrokerAdminPort { get; set; }
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


            if (ConfigurationManager.ConnectionStrings["BrokerIp"] != null)
            {
                BrokerIp = IPAddress.Parse(ConfigurationManager.AppSettings["BrokerIp"]);
            }
            if (ConfigurationManager.ConnectionStrings["BrokerProducerPort"] != null)
            {
                BrokerProducerPort = int.Parse(ConfigurationManager.AppSettings["BrokerProducerPort"]);
            }
            if (ConfigurationManager.ConnectionStrings["BrokerConsumerPort"] != null)
            {
                BrokerConsumerPort = int.Parse(ConfigurationManager.AppSettings["BrokerConsumerPort"]);
            }

            if (ConfigurationManager.ConnectionStrings["BrokerAdminPort"] != null)
            {
                BrokerAdminPort = int.Parse(ConfigurationManager.AppSettings["BrokerAdminPort"]);
            }
            if (ConfigurationManager.ConnectionStrings["CommandBindingPort"] != null)
            {
                CommandBindingPort = int.Parse(ConfigurationManager.AppSettings["CommandBindingPort"]);
            }
        }
    }
}
