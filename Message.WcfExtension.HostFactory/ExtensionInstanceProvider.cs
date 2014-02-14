using System;
using System.ServiceModel;
using System.ServiceModel.Dispatcher;
using Ninject;

namespace Message.WcfExtension.HostFactory
{
    using Message = System.ServiceModel.Channels.Message;
    /// <summary>
    /// 一个使用Ninject来创建服务实例的类
    /// </summary>
    public class ExtensionInstanceProvider : IInstanceProvider
    {

        /// <summary>
        /// 服务类型
        /// </summary>
        private readonly Type serviceType;


        private readonly IKernel kernel;


        public ExtensionInstanceProvider(Type serviceType, IKernel kernel)
        {
            this.serviceType = serviceType;
            this.kernel = kernel;
        }

        /// <summary>
        ///返回一个自定义的服务对象
        /// </summary>
        /// <param name="instanceContext"> </param>
        /// <returns>
        /// 自定义服务对象
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
        /// 当一个服务对象被回收的时候调用
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