using System.Linq;
using System.ServiceModel;

namespace Message.WcfExtension.HostFactory
{
    /// <summary>
    /// �жϷ����Ƿ��ǵ���ģʽ����
    /// </summary>
    internal static class ServiceTypeHelper
    {
        /// <summary>
        /// �жϸ����ķ����Ƿ��ǵ���ģʽ
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