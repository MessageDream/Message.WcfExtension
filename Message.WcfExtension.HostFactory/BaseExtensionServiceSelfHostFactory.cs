using System;
using Ninject;
using Ninject.Parameters;
using YesWay.WcfExtension;

namespace Message.WcfExtension.HostFactory
{

    public abstract class BaseExtensionServiceSelfHostFactory : IExtensionSelfHostFactory
    {

        private static IKernel kernelInstance;

        protected abstract Type ServiceHostType { get; }

        public static void SetKernel(IKernel kernel)
        {
            kernelInstance = kernel;
        }

        public System.ServiceModel.ServiceHost CreateServiceHost(Type serviceType, Uri[] baseAddresses)
        {
            return (System.ServiceModel.ServiceHost)kernelInstance.Get(
                this.ServiceHostType.MakeGenericType(serviceType),
                new ConstructorArgument("baseAddresses", baseAddresses));
        }
    }
}