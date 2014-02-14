using System;
using System.ServiceModel.Channels;
using System.ServiceModel.Description;
using System.ServiceModel.Dispatcher;
using Message.WcfExtension.HostFactory.Log;

namespace Message.WcfExtension.HostFactory
{
    public class ExtensionOperationBehavior : IOperationBehavior
    {

        /// <summary>
        /// 创建方法拦截器的工厂方法    
        /// </summary>
        private readonly Func<IOperationInvoker, ILog, string, IOperationInvoker> _instanceInvokerFactory;

        private readonly ILog _log;

        public ExtensionOperationBehavior(Func<IOperationInvoker, ILog, string, IOperationInvoker> instanceInvokerFactory, ILog log)
        {
            this._instanceInvokerFactory = instanceInvokerFactory;
            this._log = log;
        }

        public void Validate(OperationDescription operationDescription)
        {

        }

        public void ApplyDispatchBehavior(OperationDescription operationDescription, DispatchOperation dispatchOperation)
        {
            dispatchOperation.Invoker = this._instanceInvokerFactory(dispatchOperation.Invoker, _log, operationDescription.Name);
        }

        public void ApplyClientBehavior(OperationDescription operationDescription, ClientOperation clientOperation)
        {

        }

        public void AddBindingParameters(OperationDescription operationDescription, BindingParameterCollection bindingParameters)
        {

        }
    }
}
