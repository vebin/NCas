﻿using System.Reflection;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Autofac;
using Autofac.Integration.Mvc;
using ECommon.Autofac;
using ECommon.Components;
using ECommon.Configurations;
using ECommon.Logging;
using ENode.Configurations;
using NCas.Common;
using NCas.Web.Extensions;

namespace NCas.Web
{
    public class MvcApplication : System.Web.HttpApplication
    {
        private ILogger _logger;
        private Configuration _ecommonConfiguration;
        private ENodeConfiguration _enodeConfiguration;

        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            GlobalFilters.Filters.Add(new HandleExceptionAttribute());
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            InitializeECommon();
            InitializeENode();
        }
        private void InitializeECommon()
        {
            _ecommonConfiguration = Configuration
                .Create()
                .UseAutofac()
                .RegisterCommonComponents()
                .UseLog4Net()
                .UseJsonNet()
                .RegisterUnhandledExceptionHandler();

            _logger = ObjectContainer.Resolve<ILoggerFactory>().Create(GetType().FullName);
            _logger.Info("ECommon initialized.");
        }
        private void InitializeENode()
        {
            ConfigSettings.Initialize();

            var assemblies = new[]
            {
                Assembly.Load("NCas"),
                Assembly.Load("NCas.Core"),
                Assembly.GetExecutingAssembly()
            };

            _enodeConfiguration = _ecommonConfiguration
                .CreateENode()
                .RegisterENodeComponents()
                .RegisterBusinessComponents(assemblies)
                .UseEQueue()
                .InitializeBusinessAssemblies(assemblies)
                .StartEQueue();

            RegisterControllers();
            _logger.Info("ENode initialized.");
        }

        // ReSharper disable once MemberCanBeMadeStatic.Local
        private void RegisterControllers()
        {
            var webAssembly = Assembly.GetExecutingAssembly();
            var container = (ObjectContainer.Current as AutofacObjectContainer).Container;
            var builder = new ContainerBuilder();
            builder.RegisterControllers(webAssembly);
            builder.Update(container);
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
        }
    }
}
