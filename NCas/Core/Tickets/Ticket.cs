﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GUtils.Encrypt;

namespace NCas.Core.Tickets
{
    /// <summary>票据
    /// </summary>
    public class Ticket
    {
        /// <summary>票据Id
        /// </summary>
        public string TicketId { get; set; }

        /// <summary>票据
        /// </summary>
        public string TicketValue { get; set; }

        /// <summary>票据创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }

        /// <summary>关联账号
        /// </summary>
        public TicketAccount RefAccount { get; set; }

        /// <summary>判断票据是否过期
        /// </summary>
        public bool IsExpired(int timeoutSeconds)
        {
            return (DateTime.Now - CreateTime).TotalSeconds >= timeoutSeconds;
        }


        public Ticket(string ticketId, string ticketValue, TicketAccount refAccount)
        {
            TicketId = ticketId;
            TicketValue = ticketValue;
            RefAccount = refAccount;
            CreateTime = DateTime.Now;
        }

        public string GetEncryptTicket()
        {
            return EncryptHelper.AesEncryString(TicketValue, password: "123456");
        }

    }
}
