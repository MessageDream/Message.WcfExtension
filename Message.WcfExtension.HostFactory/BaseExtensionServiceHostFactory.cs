using System;
using System.ServiceModel.Activation;
using Ninject;
using Ninject.Parameters;

namespace Message.WcfExtension.HostFactory
{
    /// <summary>
    /// ���������������Ĺ�����
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
        /// Ϊ�������͵ķ��񴴽�һ������
        /// </summary>
        /// <param name="serviceType"> �������� </param>
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