using System;
using YesWay.WcfExtension;

namespace Message.WcfExtension.HostFactory.ServiceHost
{
    /// <summary>
    ///  ���NinjectServiceHosts�ļ��޹���
    /// </summary>
    public class ExtensionServiceHostFactory : BaseExtensionServiceHostFactory
    {
        /// <summary>
        /// ��ȡhost����
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