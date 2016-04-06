﻿using System.Net;
using ECommon.Socketing;
using ENode.Configurations;
using ENode.EQueue;
using ENode.Eventing;
using ENode.Infrastructure;
using EQueue.Clients.Consumers;
using EQueue.Clients.Producers;
using EQueue.Configurations;
using NCas.Common;

namespace NCas.ProcessorHost
{
    public static class ENodeExtensions
    {
        private static CommandConsumer _commandConsumer;
        private static ApplicationMessagePublisher _applicationMessagePublisher;
        private static DomainEventPublisher _domainEventPublisher;
        private static DomainEventConsumer _eventConsumer;
        private static PublishableExceptionPublisher _exceptionPublisher;
        private static PublishableExceptionConsumer _exceptionConsumer;

        public static ENodeConfiguration UseEQueue(this ENodeConfiguration enodeConfiguration)
        {
            var configuration = enodeConfiguration.GetCommonConfiguration();

            configuration.RegisterEQueueComponents();

            var producerSetting = new ProducerSetting
            {
                BrokerAddress = new IPEndPoint(SocketUtils.GetLocalIPV4(), ConfigSettings.BrokerProducerPort),
                BrokerAdminAddress = new IPEndPoint(SocketUtils.GetLocalIPV4(), ConfigSettings.BrokerAdminPort)
            };
            var consumerSetting = new ConsumerSetting
            {
                BrokerAddress = new IPEndPoint(SocketUtils.GetLocalIPV4(), ConfigSettings.BrokerConsumerPort),
                BrokerAdminAddress = new IPEndPoint(SocketUtils.GetLocalIPV4(), ConfigSettings.BrokerAdminPort)
            };

            _applicationMessagePublisher = new ApplicationMessagePublisher(producerSetting);
            _domainEventPublisher = new DomainEventPublisher(producerSetting);
            _exceptionPublisher = new PublishableExceptionPublisher(producerSetting);

            configuration.SetDefault<IMessagePublisher<IApplicationMessage>, ApplicationMessagePublisher>(_applicationMessagePublisher);
            configuration.SetDefault<IMessagePublisher<DomainEventStreamMessage>, DomainEventPublisher>(_domainEventPublisher);
            configuration.SetDefault<IMessagePublisher<IPublishableException>, PublishableExceptionPublisher>(_exceptionPublisher);

            _commandConsumer = new CommandConsumer("NCasCommandConsumerGroup", consumerSetting).Subscribe(Topics.NCasCommandTopic);
            _eventConsumer = new DomainEventConsumer("NCasEventConsumerGroup", consumerSetting).Subscribe(Topics.NCasDomainEventTopic);
            _exceptionConsumer = new PublishableExceptionConsumer("NCasExceptionConsumerGroup", consumerSetting).Subscribe(Topics.NCasExceptionTopic);

            return enodeConfiguration;
        }
        public static ENodeConfiguration StartEQueue(this ENodeConfiguration enodeConfiguration)
        {
            _exceptionConsumer.Start();
            _eventConsumer.Start();
            _commandConsumer.Start();
            _applicationMessagePublisher.Start();
            _domainEventPublisher.Start();
            _exceptionPublisher.Start();

            return enodeConfiguration;
        }
        public static ENodeConfiguration ShutdownEQueue(this ENodeConfiguration enodeConfiguration)
        {
            _applicationMessagePublisher.Shutdown();
            _domainEventPublisher.Shutdown();
            _exceptionPublisher.Shutdown();
            _commandConsumer.Shutdown();
            _eventConsumer.Shutdown();
            _exceptionConsumer.Shutdown();
            return enodeConfiguration;
        }
    }
}
