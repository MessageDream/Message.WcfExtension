using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Description;
using System.ServiceModel.Dispatcher;

namespace Message.WcfExtension.HostFactory
{
    /// <summary>
    /// 一个扩展的服务行为 使用Ninject.
    /// </summary>
    public class ExtensionServiceBehavior : IServiceBehavior
    {
        /// <summary>
        /// 创建服务提供者的工厂方法
        /// </summary>
        private readonly Func<Type, IInstanceProvider> _instanceProviderFactory;

        /// <summary>
        /// 这是附加到每个终节点调度员在每个操作之后来清理请求范围对象在ninject的缓存。
        /// </summary>
        private readonly IDispatchMessageInspector _requestScopeCleanUp;


        public ExtensionServiceBehavior(Func<Type, IInstanceProvider> instanceProviderFactory, IDispatchMessageInspector requestScopeCleanUp)
        {
            this._instanceProviderFactory = instanceProviderFactory;
            this._requestScopeCleanUp = requestScopeCleanUp;
        }

        /// <summary>
        /// 验证服务是否可以正常执行
        /// </summary>
        /// <param name="serviceDescription"></param>
        /// <param name="serviceHostBase"></param>
        public void Validate(ServiceDescription serviceDescription, ServiceHostBase serviceHostBase)
        {
        }

        /// <summary>
        /// 可以通过自定义数据绑定元素来支持合同实现。
        /// </summary>
        /// <param name="serviceDescription"></param>
        /// <param name="serviceHostBase"></param>
        /// <param name="endpoints"></param>
        /// <param name="bindingParameters"></param>
        public void AddBindingParameters(
            ServiceDescription serviceDescription,
            ServiceHostBase serviceHostBase,
            Collection<ServiceEndpoint> endpoints,
            BindingParameterCollection bindingParameters)
        {
        }

        /// <summary>
        /// 提供了能够改变运行时属性值或插入自定义扩展对象(如错误处理程序、消息或参数拦截器,安全扩展,和其他自定义扩展对象)。
        /// </summary>
        /// <param name="serviceDescription"></param>
        /// <param name="serviceHostBase"></param>
        public void ApplyDispatchBehavior(ServiceDescription serviceDescription, ServiceHostBase serviceHostBase)
        {
            var endpointDispatchers = serviceHostBase.ChannelDispatchers.OfType<ChannelDispatcher>().SelectMany(channelDispatcher => channelDispatcher.Endpoints);
            foreach (EndpointDispatcher endpointDispatcher in endpointDispatchers)
            {
                endpointDispatcher.DispatchRuntime.InstanceProvider = this._instanceProviderFactory(serviceDescription.ServiceType);
                endpointDispatcher.DispatchRuntime.MessageInspectors.Add(this._requestScopeCleanUp);
            }
        }
    }
}