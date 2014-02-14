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
    /// һ����չ�ķ�����Ϊ ʹ��Ninject.
    /// </summary>
    public class ExtensionServiceBehavior : IServiceBehavior
    {
        /// <summary>
        /// ���������ṩ�ߵĹ�������
        /// </summary>
        private readonly Func<Type, IInstanceProvider> _instanceProviderFactory;

        /// <summary>
        /// ���Ǹ��ӵ�ÿ���սڵ����Ա��ÿ������֮������������Χ������ninject�Ļ��档
        /// </summary>
        private readonly IDispatchMessageInspector _requestScopeCleanUp;


        public ExtensionServiceBehavior(Func<Type, IInstanceProvider> instanceProviderFactory, IDispatchMessageInspector requestScopeCleanUp)
        {
            this._instanceProviderFactory = instanceProviderFactory;
            this._requestScopeCleanUp = requestScopeCleanUp;
        }

        /// <summary>
        /// ��֤�����Ƿ��������ִ��
        /// </summary>
        /// <param name="serviceDescription"></param>
        /// <param name="serviceHostBase"></param>
        public void Validate(ServiceDescription serviceDescription, ServiceHostBase serviceHostBase)
        {
        }

        /// <summary>
        /// ����ͨ���Զ������ݰ�Ԫ����֧�ֺ�ͬʵ�֡�
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
        /// �ṩ���ܹ��ı�����ʱ����ֵ������Զ�����չ����(������������Ϣ�����������,��ȫ��չ,�������Զ�����չ����)��
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