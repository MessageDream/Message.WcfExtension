using System;
using YesWay.WcfExtension;

namespace Message.WcfExtension.HostFactory.ServiceHost
{
    /// <summary>
    ///  针对NinjectServiceHosts的寄宿工厂
    /// </summary>
    public class ExtensionServiceHostFactory : BaseExtensionServiceHostFactory
    {
        /// <summary>
        /// 获取host类型
        /// </summary>
        /// <value></value>
        protected override Type ServiceHostType
        {
            get
            {
                return typeof(ExtensionIISHostingServiceHost<>);
            }
        }
    }
}