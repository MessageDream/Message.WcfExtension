using System.Linq;
using System.ServiceModel;

namespace Message.WcfExtension.HostFactory
{
    /// <summary>
    /// 判断服务是否是单例模式的类
    /// </summary>
    internal static class ServiceTypeHelper
    {
        /// <summary>
        /// 判断给出的服务是否是单例模式
        /// </summary>
        /// <param name="service"></param>
        /// <returns></returns>
        public static bool IsSingletonService(object service)
        {
            var serviceBehaviorAttribute =
                service.GetType().GetCustomAttributes(typeof(ServiceBehaviorAttribute), true)
                .Cast<ServiceBehaviorAttribute>()
                .SingleOrDefault();
            return serviceBehaviorAttribute != null && serviceBehaviorAttribute.InstanceContextMode == InstanceContextMode.Single;
        }
    }
}