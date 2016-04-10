using System;
using System.Configuration;
using System.Net;
using ECommon.Components;
using ECommon.Configurations;
using ECommon.Logging;
using ECommon.Socketing;
using EQueue.Broker;
using EQueue.Configurations;
using NCas.Common;
using ECommonConfiguration = ECommon.Configurations.Configuration;
namespace NCas.MessageBroker
{
    public class Bootstrap
    {
        private ILogger _logger;
        private ECommonConfiguration _configuration;
        private BrokerController _broker;

        public void Initialize()
        {
            ConfigSettings.Initialize();
            InitializeECommon();
            try
            {
                InitializeEQueue();
            }
            catch (Exception ex)
            {
                _logger.Error("Initialize EQueue failed.", ex);
                throw;
            }
        }

        public void Start()
        {
            try
            {
                _broker.Start();
            }
            catch (Exception ex)
            {
                _logger.Error("Broker start failed.", ex);
                throw;
            }
        }

        public void Stop()
        {
            try
            {
                if (_broker != null)
                {
                    _broker.Shutdown();
                }
            }
            catch (Exception ex)
            {
                _logger.Error("Broker stop failed.", ex);
                throw;
            }
        }

        private void InitializeECommon()
        {
            _configuration = ECommonConfiguration
                .Create()
                .UseAutofac()
                .RegisterCommonComponents()
                .UseLog4Net()
                .UseJsonNet()
                .RegisterUnhandledExceptionHandler();
            _logger = ObjectContainer.Resolve<ILoggerFactory>().Create(typeof (Bootstrap).FullName);
            _logger.Info("ECommon initialized.");
        }

        private void InitializeEQueue()
        {
            _configuration.RegisterEQueueComponents()
                .UseDeleteMessageByCountStrategy(20);
            var storePath = ConfigurationManager.AppSettings["equeueStorePath"];
            var setting = new BrokerSetting(storePath)
            {
                ProducerAddress = new IPEndPoint(SocketUtils.GetLocalIPV4(), ConfigSettings.BrokerProducerPort),
                ConsumerAddress = new IPEndPoint(SocketUtils.GetLocalIPV4(), ConfigSettings.BrokerConsumerPort),
                AdminAddress = new IPEndPoint(SocketUtils.GetLocalIPV4(), ConfigSettings.BrokerAdminPort)
            };
            _broker = BrokerController.Create(setting);
            _logger.Info("EQueue initialized.");
        }
    }
}
