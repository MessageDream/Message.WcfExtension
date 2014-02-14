using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel.Description;
using Castle.Core.Internal;

namespace Message.WcfExtension.HostFactory.ServiceHost
{
    /// <summary>
    /// ��Ninject����������ʵ������
    /// </summary>
    public class ExtensionServiceHost : System.ServiceModel.ServiceHost
    {
        /// <summary>
        /// ������Ϊ
        /// </summary>
        private readonly IEnumerable<IServiceBehavior> _serviceBehaviors;

        /// <summary>
        /// �ս����Ϊ
        /// </summary>
        private readonly IEnumerable<IEndpointBehavior> _endpointBehaviors;

        /// <summary>
        /// ������Ϊ
        /// </summary>
        private readonly IEnumerable<IOperationBehavior> _operationBehaviors;

        #region ���캯������������Ϊ��
        /// <summary>
        /// ��ʼ��һ���µ�ʵ��
        /// </summary>
        /// <param name="serviceBehaviors">������Ϊ</param>
        public ExtensionServiceHost(IEnumerable<IServiceBehavior> serviceBehaviors)
        {
            this._serviceBehaviors = serviceBehaviors;
        }

        /// <summary>
        /// ��ʼ��һ���µ�ʵ��
        /// </summary>
        /// <param name="serviceBehaviors">������Ϊ</param>
        /// <param name="serviceType">��������</param>
        public ExtensionServiceHost(IEnumerable<IServiceBehavior> serviceBehaviors, TypeCode serviceType)
            : base(serviceType)
        {
            this._serviceBehaviors = serviceBehaviors;
        }

        /// <summary>
        /// ��ʼ��һ���µ�ʵ��
        /// </summary>
        /// <param name="serviceBehaviors">������Ϊ</param>
        /// <param name="singletonInstance">����</param>
        public ExtensionServiceHost(IEnumerable<IServiceBehavior> serviceBehaviors, object singletonInstance)
            : base(singletonInstance)
        {
            this._serviceBehaviors = serviceBehaviors;
        }

        /// <summary>
        /// ��ʼ��һ���µ�ʵ��
        /// </summary>
        /// <param name="serviceBehaviors">������Ϊ</param>
        /// <param name="serviceType">��������</param>
        /// <param name="baseAddresses">����ַ</param>
        public ExtensionServiceHost(IEnumerable<IServiceBehavior> serviceBehaviors, Type serviceType, params Uri[] baseAddresses)
            : base(serviceType, baseAddresses)
        {
            this._serviceBehaviors = serviceBehaviors;
        }
        #endregion

        #region ���캯�������ս����Ϊ��

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

        #region ���캯������������Ϊ��

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

        #region ���캯������Ϊ��ϣ�

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
        /// �ڹ����ڼ���õ�ͨ�Ŷ��󵽴�״̬��
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