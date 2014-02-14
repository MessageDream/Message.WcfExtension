using System;
using YesWay.WcfExtension;

namespace Message.WcfExtension.HostFactory.ServiceHost
{
    /// <summary>
    /// 针对 NinjectServiceHosts 的自承载宿主工厂
    /// </summary>
    public class ExtensionServiceSelfHostFactory : BaseExtensionServiceSelfHostFactory
    {
        /// <summary>
        /// 获取宿主类型
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