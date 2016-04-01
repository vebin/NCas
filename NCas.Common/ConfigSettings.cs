﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
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
        /// <summary>账号索引表
        /// </summary>
        public static string AccountIndexTable { get; set; }


        public static int BrokerProducerPort { get; set; }
        public static int BrokerConsumerPort { get; set; }
        public static int BrokerAdminPort { get; set; }

        public static void Initialize()
        {
            if (ConfigurationManager.ConnectionStrings["Enode"] != null)
            {
                ENodeConnectionString = ConfigurationManager.ConnectionStrings["Enode"].ConnectionString;
            }

            if (ConfigurationManager.ConnectionStrings["Cas"] != null)
            {
                CasConnectionString = ConfigurationManager.ConnectionStrings["Cas"].ConnectionString;
            }

            AccountTable = "Account";


            BrokerProducerPort = 10000;
            BrokerConsumerPort = 10001;
            BrokerAdminPort = 10002;
        }
    }
}