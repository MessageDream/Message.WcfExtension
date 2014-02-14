using System;
using System.ServiceModel;
using System.ServiceModel.Dispatcher;
using Ninject;

namespace Message.WcfExtension.HostFactory
{
    using Message = System.ServiceModel.Channels.Message;
    /// <summary>
    /// һ��ʹ��Ninject����������ʵ������
    /// </summary>
    public class ExtensionInstanceProvider : IInstanceProvider
    {

        /// <summary>
        /// ��������
        /// </summary>
        private readonly Type serviceType;


        private readonly IKernel kernel;


        public ExtensionInstanceProvider(Type serviceType, IKernel kernel)
        {
            this.serviceType = serviceType;
            this.kernel = kernel;
        }

        /// <summary>
        ///����һ���Զ���ķ������
        /// </summary>
        /// <param name="instanceContext"> </param>
        /// <returns>
        /// �Զ���������
        /// </returns>
        public object GetInstance(InstanceContext instanceContext)
        {
            return this.GetInstance(instanceContext, null);
        }


        public object GetInstance(InstanceContext instanceContext, Message message)
        {
            return this.kernel.Get(this.serviceType);
        }

        /// <summary>
        /// ��һ��������󱻻��յ�ʱ�����
        /// </summary>
        /// <param name="instanceContext"> </param>
        /// <param name="instance"> </param>
        public void ReleaseInstance(InstanceContext instanceContext, object instance)
        {
            if (!this.kernel.Release(instance))
            {
                IDisposable disposable = instance as IDisposable;
                if (disposable != null)
                {
                    disposable.Dispose();
                }
            }
        }
    }
}