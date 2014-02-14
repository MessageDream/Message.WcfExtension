using System;
using System.Collections.Generic;
using System.ServiceModel.Description;

namespace Message.WcfExtension.HostFactory.ServiceHost
{
    /// <summary>
    /// ��Ninject����������ʵ������
    /// </summary>
    public class ExtensionServiceHost<T> : ExtensionAbstractServiceHost<T>
    {
        /// <summary>
        /// ��ʼ��һ���µ�ʵ�� <see cref="ExtensionServiceHost"/>
        /// </summary>
        /// <param name="serviceBehaviors">������Ϊ</param>
        /// <param name="instance">ʵ��</param>
        public ExtensionServiceHost(IEnumerable<IServiceBehavior> serviceBehaviors, T instance)
            : base(serviceBehaviors, instance, new Uri[0])
        {
        }
    }
}