using System;
using System.ServiceModel.Activation;
using Ninject;
using Ninject.Parameters;

namespace Message.WcfExtension.HostFactory
{
    /// <summary>
    /// 针对特殊服务宿主的工厂类
    /// </summary>
    public abstract class BaseExtensionServiceHostFactory : ServiceHostFactory
    {

        private static IKernel kernelInstance;


        protected abstract Type ServiceHostType { get; }


        public static void SetKernel(IKernel kernel)
        {
            kernelInstance = kernel;
        }

        /// <summary>
        /// 为特殊类型的服务创建一个宿主
        /// </summary>
        /// <param name="serviceType"> 服务类型 </param>
        /// <param name="baseAddresses"></param>
        /// <returns></returns>
        protected override System.ServiceModel.ServiceHost CreateServiceHost(Type serviceType, Uri[] baseAddresses)
        {
            return (System.ServiceModel.ServiceHost)kernelInstance.Get(
                this.ServiceHostType.MakeGenericType(serviceType),
                new ConstructorArgument("baseAddresses", baseAddresses));
        }
    }
}