using System;

namespace Message.WcfExtension.HostFactory
{
    /// <summary>
    /// 自承载服务宿主工厂
    /// </summary>
    public interface IExtensionSelfHostFactory
    {
        /// <summary>
        /// 在指定类型的服务和一个特定的基地址 创建一个 ServiceHost。
        /// </summary>
        /// <param name="serviceType"></param>
        /// <param name="baseAddresses"></param>
        /// <returns></returns>
        System.ServiceModel.ServiceHost CreateServiceHost(Type serviceType, Uri[] baseAddresses);
    }
}