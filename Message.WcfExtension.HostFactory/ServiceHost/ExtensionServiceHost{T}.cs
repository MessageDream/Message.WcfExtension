using System;
using System.Collections.Generic;
using System.ServiceModel.Description;

namespace Message.WcfExtension.HostFactory.ServiceHost
{
    /// <summary>
    /// 用Ninject来创建服务实例的类
    /// </summary>
    public class ExtensionServiceHost<T> : ExtensionAbstractServiceHost<T>
    {
        /// <summary>
        /// 初始化一个新的实例 <see cref="ExtensionServiceHost"/>
        /// </summary>
        /// <param name="serviceBehaviors">服务行为</param>
        /// <param name="instance">实例</param>
        public ExtensionServiceHost(IEnumerable<IServiceBehavior> serviceBehaviors, T instance)
            : base(serviceBehaviors, instance, new Uri[0])
        {
        }
    }
}