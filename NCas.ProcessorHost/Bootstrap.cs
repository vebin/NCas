﻿using System;
using System.Reflection;
using ECommon.Components;
using ECommon.Configurations;
using ECommon.Logging;
using ENode.Configurations;
using ENode.Infrastructure;
using ENode.MySqlExtensions;
using NCas.Common;
using NCas.Domain.Accounts;
using ECommonConfiguration = ECommon.Configurations.Configuration;
namespace NCas.ProcessorHost
{
    public class Bootstrap
    {
        private  ILogger _logger;
        private  ECommonConfiguration _ecommonConfiguration;
        private  ENodeConfiguration _enodeConfiguration;

        public  void Initialize()
        {
            InitializeECommon();
            try
            {
                InitializeENode();
            }
            catch (Exception ex)
            {
                _logger.Error("Initialize ENode failed.", ex);
                throw;
            }
        }
        public  void Start()
        {
            try
            {
                _enodeConfiguration.StartEQueue();
            }
            catch (Exception ex)
            {
                _logger.Error("EQueue start failed.", ex);
                throw;
            }
        }
        public  void Stop()
        {
            try
            {
                _enodeConfiguration.ShutdownEQueue();
            }
            catch (Exception ex)
            {
                _logger.Error("EQueue stop failed.", ex);
                throw;
            }
        }

        private  void InitializeECommon()
        {
            _ecommonConfiguration = ECommonConfiguration
                .Create()
                .UseAutofac()
                .RegisterCommonComponents()
                .UseLog4Net()
                .UseJsonNet()
                .RegisterUnhandledExceptionHandler();
            _logger = ObjectContainer.Resolve<ILoggerFactory>().Create(typeof(Bootstrap).FullName);
            _logger.Info("ECommon initialized.");
        }

        private void InitializeENode()
        {
              ConfigSettings.Initialize();

            var assemblies = new[]
            {
                Assembly.Load("NCas"),
                Assembly.GetExecutingAssembly()
            };
            var setting = new ConfigurationSetting
            {
                DefaultDBConfigurationSetting =new DefaultDBConfigurationSetting(ConfigSettings.ENodeConnectionString)
      
            };

            _enodeConfiguration = _ecommonConfiguration
                .CreateENode(setting)
                .RegisterENodeComponents()
                .RegisterBusinessComponents(assemblies)
                //.UseSqlServerLockService()
                //.UseSqlServerCommandStore()
                //.UseSqlServerEventStore()
                //.UseSqlServerPublishedVersionStore()
                .UseMySqlLockService()
                .UseMySqlCommandStore()
                .UseMySqlEventStore()
                .UseMySqlPublishedVersionStore()
                .UseEQueue()
                .InitializeBusinessAssemblies(assemblies);

            #region 锁

            ObjectContainer.Resolve<ILockService>().AddLockKey(typeof (Account).Name);
            //ObjectContainer.Resolve<ILockService>().AddLockKey(typeof(Position).Name);

            //ObjectContainer.Resolve<ILockService>().AddLockKey(typeof(Subject).Name);
            //ObjectContainer.Resolve<ILockService>().AddLockKey(typeof(SubjectAttribute).Name);
            //ObjectContainer.Resolve<ILockService>().AddLockKey(typeof(SubjectAtom).Name);

            //ObjectContainer.Resolve<ILockService>().AddLockKey(typeof(School).Name);
            //ObjectContainer.Resolve<ILockService>().AddLockKey(typeof(SchoolPosition).Name);
            //ObjectContainer.Resolve<ILockService>().AddLockKey(typeof(Teacher).Name);

            #endregion

            _logger.Info("ENode initialized.");
        }
    }
}
