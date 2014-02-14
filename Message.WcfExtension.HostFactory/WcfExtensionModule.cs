using System;
using System.ServiceModel.Description;
using System.ServiceModel.Dispatcher;
using Message.WcfExtension.HostFactory.Cache;
using Message.WcfExtension.HostFactory.Log;
using Ninject;
using Ninject.Extensions.Interception;
using Ninject.Parameters;
using Ninject.Web.Common;
using NLog;
using YesWay.WcfExtension;
using YesWay.WcfExtension.Cache;

namespace Message.WcfExtension.HostFactory
{
    /// <summary>
    /// 为WCF扩展创建服务绑定的注入模块   
    /// </summary>
    public class WcfExtensionModule : GlobalKernelRegistrationModule<WcfRequestScopeCleanup>
    {
        /// <summary>
        ///加载模块到注入内核容器
        /// </summary>
        public override void Load()
        {
            base.Load();
            Kernel.Components.Add<INinjectHttpApplicationPlugin, ExtensionWcfHttpApplicationPlugin>();
            this.Bind<ExtensionInstanceProvider>().ToSelf();

            #region 日志
            this.Bind<ExtensionOperationInvoker>().ToSelf();

            this.Bind<Logger>().ToMethod(ctx => LogManager.GetLogger("LogHelper"));

            this.Bind<ILog>().To<NLogger>();
            #endregion

            #region 缓存
            this.Bind<ICacheKeyGenerator>().To<DefaultCacheKeyGenerator>();
            // this.Bind<IInvocation>().To<CacheInvocation>();
            this.Bind<IInterceptor>().To<CacheInterceptor>();

            this.Bind<TimeSpan>().ToMethod(ctx => new TimeSpan(0, 5, 0));
            #endregion


            //this.Bind<ExtensionServiceBehavior>().ToSelf();
            //this.Bind<ExtensionEndpointBehavior>().ToSelf();
            //this.Bind<ExtensionOperationBehavior>().ToSelf();

            this.Bind<IServiceBehavior>().To<ExtensionServiceBehavior>();
            this.Bind<IEndpointBehavior>().To<ExtensionEndpointBehavior>();
            this.Bind<IOperationBehavior>().To<ExtensionOperationBehavior>();

            //this.Bind<IEnumerable<IServiceBehavior>>().ToMethod(ctx =>ctx.Kernel.Get<ExtensionServiceBehavior>());
            //this.Bind<IEnumerable<IEndpointBehavior>>().ToMethod(ctx => new [] { ctx.Kernel.Get<ExtensionEndpointBehavior>() });
            //this.Bind<IEnumerable<IOperationBehavior>>().ToMethod(ctx => new [] { ctx.Kernel.Get<ExtensionOperationBehavior>() });

            this.Bind<IDispatchMessageInspector>().To<WcfRequestScopeCleanup>()
                .WithConstructorArgument("releaseScopeAtRequestEnd", ctx => ctx.Kernel.Settings.Get("ReleaseScopeAtRequestEnd", true));

            this.Bind<Func<Type, IInstanceProvider>>()
                .ToMethod(ctx => serviceType => ctx.Kernel.Get<ExtensionInstanceProvider>(new ConstructorArgument("serviceType", serviceType)));

            this.Bind<Func<IOperationInvoker, ILog, string, IOperationInvoker>>()
                .ToMethod(ctx => (invoker, log, operationName) => ctx.Kernel.Get<ExtensionOperationInvoker>(new ConstructorArgument("invoker", invoker), new ConstructorArgument("log", log), new ConstructorArgument("operationName", operationName)));

        }
    }
}