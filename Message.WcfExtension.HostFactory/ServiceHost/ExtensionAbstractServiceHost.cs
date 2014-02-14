using System;
using System.Collections.Generic;
using System.ServiceModel;
using System.ServiceModel.Description;
using YesWay.WcfExtension;

namespace Message.WcfExtension.HostFactory.ServiceHost
{
    /// <summary>
    /// 抽象基类
    /// </summary>
    /// <typeparam name="T">service类型</typeparam>
    public abstract class ExtensionAbstractServiceHost<T> : ExtensionServiceHost
    {
        protected ExtensionAbstractServiceHost(IEnumerable<IServiceBehavior> serviceBehaviors, T instance, Uri[] baseAddresses)
            : base(serviceBehaviors)
        {
            var addresses = new UriSchemeKeyedCollection(baseAddresses);

            if (ServiceTypeHelper.IsSingletonService(instance))
            {
                this.InitializeDescription(instance, addresses);
            }
            else
            {
                this.InitializeDescription(typeof(T), addresses);
            }
        }

        protected ExtensionAbstractServiceHost(IEnumerable<IEndpointBehavior> endpointBehaviors, T instance, Uri[] baseAddresses)
            : base(endpointBehaviors)
        {
            var addresses = new UriSchemeKeyedCollection(baseAddresses);

            if (ServiceTypeHelper.IsSingletonService(instance))
            {
                this.InitializeDescription(instance, addresses);
            }
            else
            {
                this.InitializeDescription(typeof(T), addresses);
            }
        }

        protected ExtensionAbstractServiceHost(IEnumerable<IOperationBehavior> operationBehaviors, T instance, Uri[] baseAddresses)
            : base(operationBehaviors)
        {
            var addresses = new UriSchemeKeyedCollection(baseAddresses);

            if (ServiceTypeHelper.IsSingletonService(instance))
            {
                this.InitializeDescription(instance, addresses);
            }
            else
            {
                this.InitializeDescription(typeof(T), addresses);
            }
        }

        protected ExtensionAbstractServiceHost(IEnumerable<IServiceBehavior> serviceBehaviors, IEnumerable<IEndpointBehavior> endpointBehaviors, T instance, Uri[] baseAddresses)
            : base(serviceBehaviors, endpointBehaviors)
        {
            var addresses = new UriSchemeKeyedCollection(baseAddresses);

            if (ServiceTypeHelper.IsSingletonService(instance))
            {
                this.InitializeDescription(instance, addresses);
            }
            else
            {
                this.InitializeDescription(typeof(T), addresses);
            }
        }

        protected ExtensionAbstractServiceHost(IEnumerable<IServiceBehavior> serviceBehaviors, IEnumerable<IEndpointBehavior> endpointBehaviors, IEnumerable<IOperationBehavior> operationBehaviors, T instance, Uri[] baseAddresses)
            : base(serviceBehaviors, endpointBehaviors, operationBehaviors)
        {
            var addresses = new UriSchemeKeyedCollection(baseAddresses);

            if (ServiceTypeHelper.IsSingletonService(instance))
            {
                this.InitializeDescription(instance, addresses);
            }
            else
            {
                this.InitializeDescription(typeof(T), addresses);
            }
        }

        protected ExtensionAbstractServiceHost(IEnumerable<IEndpointBehavior> endpointBehaviors, IEnumerable<IOperationBehavior> operationBehaviors, T instance, Uri[] baseAddresses)
            : base(endpointBehaviors, operationBehaviors)
        {
            var addresses = new UriSchemeKeyedCollection(baseAddresses);

            if (ServiceTypeHelper.IsSingletonService(instance))
            {
                this.InitializeDescription(instance, addresses);
            }
            else
            {
                this.InitializeDescription(typeof(T), addresses);
            }
        }

        protected ExtensionAbstractServiceHost(IEnumerable<IServiceBehavior> serviceBehaviors, IEnumerable<IOperationBehavior> operationBehaviors, T instance, Uri[] baseAddresses)
            : base(serviceBehaviors, operationBehaviors)
        {
            var addresses = new UriSchemeKeyedCollection(baseAddresses);

            if (ServiceTypeHelper.IsSingletonService(instance))
            {
                this.InitializeDescription(instance, addresses);
            }
            else
            {
                this.InitializeDescription(typeof(T), addresses);
            }
        }
    }
}