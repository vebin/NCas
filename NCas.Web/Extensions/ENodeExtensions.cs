﻿using System.Net;
using ECommon.Socketing;
using ENode.Commanding;
using ENode.Configurations;
using ENode.EQueue;
using EQueue.Clients.Producers;
using EQueue.Configurations;
using NCas.Common;

namespace NCas.Web.Extensions
{
    public static class ENodeExtensions
    {
        private static CommandService _commandService;

        public static ENodeConfiguration UseEQueue(this ENodeConfiguration enodeConfiguration)
        {
            var configuration = enodeConfiguration.GetCommonConfiguration();

            configuration.RegisterEQueueComponents();

            _commandService = new CommandService(
                new CommandResultProcessor(new IPEndPoint(ConfigSettings.BrokerIp, ConfigSettings.CommandBindingPort)),
                new ProducerSetting
                {
                    BrokerAddress = new IPEndPoint(ConfigSettings.BrokerIp, ConfigSettings.ProducerPort),
                    BrokerAdminAddress = new IPEndPoint(ConfigSettings.BrokerIp, ConfigSettings.AdminPort)
                });
            configuration.SetDefault<ICommandService, CommandService>(_commandService);
            return enodeConfiguration;
        }

        public static ENodeConfiguration StartEQueue(this ENodeConfiguration enodeConfiguration)
        {
            _commandService.Start();
            return enodeConfiguration;
        }
    }
}