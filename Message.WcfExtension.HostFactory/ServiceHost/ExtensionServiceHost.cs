using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel.Description;
using Castle.Core.Internal;

namespace Message.WcfExtension.HostFactory.ServiceHost
{
    /// <summary>
    /// 用Ninject来创建服务实例的类
    /// </summary>
    public class ExtensionServiceHost : System.ServiceModel.ServiceHost
    {
        /// <summary>
        /// 服务行为
        /// </summary>
        private readonly IEnumerable<IServiceBehavior> _serviceBehaviors;

        /// <summary>
        /// 终结点行为
        /// </summary>
        private readonly IEnumerable<IEndpointBehavior> _endpointBehaviors;

        /// <summary>
        /// 操作行为
        /// </summary>
        private readonly IEnumerable<IOperationBehavior> _operationBehaviors;

        #region 构造函数（带服务行为）
        /// <summary>
        /// 初始化一个新的实例
        /// </summary>
        /// <param name="serviceBehaviors">服务行为</param>
        public ExtensionServiceHost(IEnumerable<IServiceBehavior> serviceBehaviors)
        {
            this._serviceBehaviors = serviceBehaviors;
        }

        /// <summary>
        /// 初始化一个新的实例
        /// </summary>
        /// <param name="serviceBehaviors">服务行为</param>
        /// <param name="serviceType">服务类型</param>
        public ExtensionServiceHost(IEnumerable<IServiceBehavior> serviceBehaviors, TypeCode serviceType)
            : base(serviceType)
        {
            this._serviceBehaviors = serviceBehaviors;
        }

        /// <summary>
        /// 初始化一个新的实例
        /// </summary>
        /// <param name="serviceBehaviors">服务行为</param>
        /// <param name="singletonInstance">单例</param>
        public ExtensionServiceHost(IEnumerable<IServiceBehavior> serviceBehaviors, object singletonInstance)
            : base(singletonInstance)
        {
            this._serviceBehaviors = serviceBehaviors;
        }

        /// <summary>
        /// 初始化一个新的实例
        /// </summary>
        /// <param name="serviceBehaviors">服务行为</param>
        /// <param name="serviceType">服务类型</param>
        /// <param name="baseAddresses">基地址</param>
        public ExtensionServiceHost(IEnumerable<IServiceBehavior> serviceBehaviors, Type serviceType, params Uri[] baseAddresses)
            : base(serviceType, baseAddresses)
        {
            this._serviceBehaviors = serviceBehaviors;
        }
        #endregion

        #region 构造函数（带终结点行为）

        public ExtensionServiceHost(IEnumerable<IEndpointBehavior> endpointBehaviors)
        {
            this._endpointBehaviors = endpointBehaviors;
        }


        public ExtensionServiceHost(IEnumerable<IEndpointBehavior> endpointBehaviors, TypeCode serviceType)
            : base(serviceType)
        {
            this._endpointBehaviors = endpointBehaviors;
        }


        public ExtensionServiceHost(IEnumerable<IEndpointBehavior> endpointBehaviors, object singletonInstance)
            : base(singletonInstance)
        {
            this._endpointBehaviors = endpointBehaviors;
        }


        public ExtensionServiceHost(IEnumerable<IEndpointBehavior> endpointBehaviors, Type serviceType, params Uri[] baseAddresses)
            : base(serviceType, baseAddresses)
        {
            this._endpointBehaviors = endpointBehaviors;
        }
        #endregion

        #region 构造函数（带操作行为）

        public ExtensionServiceHost(IEnumerable<IOperationBehavior> operationBehaviors)
        {
            this._operationBehaviors = operationBehaviors;
        }


        public ExtensionServiceHost(IEnumerable<IOperationBehavior> operationBehaviors, TypeCode serviceType)
            : base(serviceType)
        {
            this._operationBehaviors = operationBehaviors;
        }


        public ExtensionServiceHost(IEnumerable<IOperationBehavior> operationBehaviors, object singletonInstance)
            : base(singletonInstance)
        {
            this._operationBehaviors = operationBehaviors;
        }


        public ExtensionServiceHost(IEnumerable<IOperationBehavior> operationBehaviors, Type serviceType, params Uri[] baseAddresses)
            : base(serviceType, baseAddresses)
        {
            this._operationBehaviors = operationBehaviors;
        }
        #endregion

        #region 构造函数（行为组合）

        public ExtensionServiceHost(IEnumerable<IServiceBehavior> serviceBehaviors, IEnumerable<IEndpointBehavior> endpointBehaviors, IEnumerable<IOperationBehavior> operationBehaviors)
        {
            this._serviceBehaviors = serviceBehaviors;
            this._endpointBehaviors = endpointBehaviors;
            this._operationBehaviors = operationBehaviors;
        }

        public ExtensionServiceHost(IEnumerable<IServiceBehavior> serviceBehaviors, IEnumerable<IEndpointBehavior> endpointBehaviors)
        {
            this._serviceBehaviors = serviceBehaviors;
            this._endpointBehaviors = endpointBehaviors;
        }

        public ExtensionServiceHost(IEnumerable<IEndpointBehavior> endpointBehaviors, IEnumerable<IOperationBehavior> operationBehaviors)
        {
            this._endpointBehaviors = endpointBehaviors;
            this._operationBehaviors = operationBehaviors;
        }

        public ExtensionServiceHost(IEnumerable<IServiceBehavior> serviceBehaviors, IEnumerable<IOperationBehavior> operationBehaviors)
        {
            this._serviceBehaviors = serviceBehaviors;
            this._operationBehaviors = operationBehaviors;
        }
        #endregion

        /// <summary>
        /// 在过渡期间调用的通信对象到打开状态。
        /// </summary>
        protected override void OnOpening()
        {
            var behaviors = Description.Behaviors;
            if (behaviors != null)
            {
                if (this._serviceBehaviors != null && this._serviceBehaviors.Any())
                    this._serviceBehaviors.ForEach(behaviors.Add);
            }
            var endpoints = Description.Endpoints;
            if (endpoints != null)
            {
                foreach (var endpoint in endpoints)
                {
                    if (this._endpointBehaviors != null && this._endpointBehaviors.Any())
                        this._endpointBehaviors.ForEach(endpoint.Behaviors.Add);
                    if (this._operationBehaviors != null && this._operationBehaviors.Any()) ;
                    foreach (var operation in endpoint.Contract.Operations)
                    {
                        this._operationBehaviors.ForEach(operation.Behaviors.Add);
                    }
                }
            }
            base.OnOpening();
        }
    }
}