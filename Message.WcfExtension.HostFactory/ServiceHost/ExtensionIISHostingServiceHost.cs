using System;
using System.Collections.Generic;
using System.ServiceModel.Description;

namespace Message.WcfExtension.HostFactory.ServiceHost
{
    /// <summary>
    /// ÔÚIISÉÏµÄServiceHost
    /// </summary>
    /// <typeparam name="T"></typeparam>
    internal class ExtensionIISHostingServiceHost<T> : ExtensionAbstractServiceHost<T>
    {
        public ExtensionIISHostingServiceHost(IEnumerable<IServiceBehavior> serviceBehaviors, T instance, Uri[] baseAddresses)
            : base(serviceBehaviors, instance, baseAddresses)
        {
        }

        public ExtensionIISHostingServiceHost(IEnumerable<IEndpointBehavior> endpointBehaviors, T instance, Uri[] baseAddresses)
            : base(endpointBehaviors, instance, baseAddresses)
        {

        }

        public ExtensionIISHostingServiceHost(IEnumerable<IOperationBehavior> operationBehaviors, T instance, Uri[] baseAddresses)
            : base(operationBehaviors, instance, baseAddresses)
        {

        }

        public ExtensionIISHostingServiceHost(IEnumerable<IServiceBehavior> serviceBehaviors, IEnumerable<IEndpointBehavior> endpointBehaviors, T instance, Uri[] baseAddresses)
            : base(serviceBehaviors, endpointBehaviors, instance, baseAddresses)
        {

        }

        public ExtensionIISHostingServiceHost(IEnumerable<IServiceBehavior> serviceBehaviors, IEnumerable<IEndpointBehavior> endpointBehaviors, IEnumerable<IOperationBehavior> operationBehaviors, T instance, Uri[] baseAddresses)
            : base(serviceBehaviors, endpointBehaviors, operationBehaviors, instance, baseAddresses)
        {

        }

        public ExtensionIISHostingServiceHost(IEnumerable<IEndpointBehavior> endpointBehaviors, IEnumerable<IOperationBehavior> operationBehaviors, T instance, Uri[] baseAddresses)
            : base(endpointBehaviors, operationBehaviors, instance, baseAddresses)
        {

        }

        public ExtensionIISHostingServiceHost(IEnumerable<IServiceBehavior> serviceBehaviors, IEnumerable<IOperationBehavior> operationBehaviors, T instance, Uri[] baseAddresses)
            : base(serviceBehaviors, operationBehaviors, instance, baseAddresses)
        {

        }
    }
}